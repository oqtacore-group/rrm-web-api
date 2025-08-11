using Microsoft.EntityFrameworkCore;
using Oqtacore.Rrm.Application.ServiceAgent;
using Oqtacore.Rrm.Infrastructure.Data;

namespace Oqtacore.Rrm.Api.ServiceWorkers
{
    // Platrum metrics
    public class MetricsService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<MetricsService> _logger;
        private readonly PlatrumApiClient _platrumApiClient;

        public MetricsService(IServiceScopeFactory scopeFactory, ILogger<MetricsService> logger, PlatrumApiClient platrumApiClient)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
            _platrumApiClient = platrumApiClient;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("MetricsService started at {Time}.", DateTime.UtcNow);

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var now = DateTime.UtcNow;
                    var nextRun = now.Date.AddDays(now.Hour >= 3 ? 1 : 0).AddHours(3);
                    var delay = nextRun - now;

                    if (delay.TotalMilliseconds <= 0)
                    {
                        delay = TimeSpan.FromHours(24);
                    }

                    _logger.LogInformation("Next run scheduled for {NextRun}. Delay: {Delay}", nextRun, delay);
                    await Task.Delay(delay, stoppingToken);

                    _logger.LogInformation("Metrics processing started at {Time}.", DateTime.UtcNow);

                    await using var scope = _scopeFactory.CreateAsyncScope();
                    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

                    await using var transaction = await dbContext.Database.BeginTransactionAsync(stoppingToken);

                    _logger.LogInformation("Attempting to acquire distributed lock...");
                    var lockAcquired = await dbContext.Database.ExecuteSqlRawAsync(
                        "EXEC sp_getapplock @Resource = 'MetricsServiceLock', @LockMode = 'Exclusive', @LockTimeout = 0;", stoppingToken);

                    if (lockAcquired == 1)
                    {
                        _logger.LogInformation("Distributed lock acquired.");

                        try
                        {
                            var yesterday = DateTime.UtcNow.Date.AddDays(-1);
                            var today = DateTime.UtcNow.Date;

                            _logger.LogInformation("Counting errors from {Start} to {End}.", yesterday, today);

                            var total4xx = await dbContext.ErrorLogs.CountAsync(
                                e => e.StatusCode >= 400 && e.StatusCode < 500 && e.Timestamp >= yesterday && e.Timestamp < today,
                                stoppingToken);

                            var total5xx = await dbContext.ErrorLogs.CountAsync(
                                e => e.StatusCode >= 500 && e.Timestamp >= yesterday && e.Timestamp < today,
                                stoppingToken);

                            _logger.LogInformation("Found {Total4xx} 4xx errors and {Total5xx} 5xx errors.", total4xx, total5xx);

                            _logger.LogInformation("Sending 4xx metrics to Platrum...");
                            var response4xx = await RetryOnFailure(() => _platrumApiClient.SendToPlatrum400(total4xx));
                            _logger.LogInformation("Sending 5xx metrics to Platrum...");
                            var response5xx = await RetryOnFailure(() => _platrumApiClient.SendToPlatrum500(total5xx));

                            if (response4xx.IsSuccessStatusCode && response5xx.IsSuccessStatusCode)
                            {
                                _logger.LogInformation("Metrics sent successfully.");
                            }
                            else
                            {
                                _logger.LogWarning("Failed to send metrics. Responses: 4xx={Status4xx}, 5xx={Status5xx}",
                                    response4xx.StatusCode, response5xx.StatusCode);
                            }

                            var oneMonthAgo = DateTime.UtcNow.AddMonths(-1);
                            const int batchSize = 1000;
                            int totalDeleted = 0;

                            _logger.LogInformation("Deleting error logs older than {OneMonthAgo}.", oneMonthAgo);

                            while (true)
                            {
                                var oldRecords = await dbContext.ErrorLogs
                                    .Where(e => e.Timestamp < oneMonthAgo)
                                    .OrderBy(e => e.Id)
                                    .Take(batchSize)
                                    .ToListAsync(stoppingToken);

                                if (!oldRecords.Any())
                                    break;

                                dbContext.ErrorLogs.RemoveRange(oldRecords);
                                await dbContext.SaveChangesAsync(stoppingToken);

                                totalDeleted += oldRecords.Count;
                                _logger.LogInformation("{Count} old records deleted in this batch.", oldRecords.Count);
                            }

                            _logger.LogInformation("Total old records deleted: {TotalDeleted}.", totalDeleted);
                        }
                        finally
                        {
                            _logger.LogInformation("Releasing distributed lock.");
                            await dbContext.Database.ExecuteSqlRawAsync("EXEC sp_releaseapplock @Resource = 'MetricsServiceLock';", stoppingToken);
                        }
                    }
                    else
                    {
                        _logger.LogWarning("Another instance is already processing metrics.");
                    }
                    await transaction.CommitAsync(stoppingToken);

                    _logger.LogInformation("Metrics processing finished at {Time}.", DateTime.UtcNow);
                }
                catch (TaskCanceledException)
                {
                    _logger.LogInformation("MetricsService was canceled.");
                    break;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred during metrics processing.");
                }

                _logger.LogInformation("Waiting for one day before next iteration.");
                await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
            }

            _logger.LogInformation("MetricsService stopped at {Time}.", DateTime.UtcNow);
        }

        private async Task<HttpResponseMessage> RetryOnFailure(Func<Task<HttpResponseMessage>> action, int maxRetries = 3)
        {
            for (int attempt = 1; attempt <= maxRetries; attempt++)
            {
                try
                {
                    return await action();
                }
                catch (Exception ex) when (attempt < maxRetries)
                {
                    _logger.LogWarning(ex, "Attempt {Attempt} failed. Retrying...", attempt);
                    await Task.Delay(TimeSpan.FromSeconds(2));
                }
            }

            throw new Exception("Max retry attempts exceeded.");
        }
    }
}
