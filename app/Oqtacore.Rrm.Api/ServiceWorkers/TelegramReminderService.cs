using Oqtacore.Rrm.Api.Models;
using Oqtacore.Rrm.Domain.Models;
using Oqtacore.Rrm.Infrastructure.Data;
using System.Net;
using TimeZoneConverter;

namespace Oqtacore.Rrm.Api.ServiceWorkers
{

    public class TelegramReminderService : IHostedService, IDisposable
    {

        private Timer _timer;
        private readonly IServiceScopeFactory scopeFactory;
        private readonly IConfiguration _configuration;
        private readonly ILogger<TelegramReminderService> _logger;

        public TelegramReminderService(IServiceScopeFactory scopeFactory, IConfiguration configuration, ILogger<TelegramReminderService> logger)
        {
            this.scopeFactory = scopeFactory;
            this._configuration = configuration;
            this._logger = logger;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("TelegramReminderService is starting.");
            _timer = new System.Threading.Timer(DoWork, "",
                1000 * 60 * 1, 1000 * 60 * 1);
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            _logger.LogInformation("TelegramReminderService timer triggered at {Time}.", DateTime.UtcNow);

            var applicationDomainUrl = _configuration["ApplicationDomainUrl"];
            using (var scope = scopeFactory.CreateScope())
            {
                ApplicationContext thql = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                DateTime checkLimit = DateTime.UtcNow.AddMinutes(5);
                DateTime currentTime = DateTime.UtcNow;
                var eventList = thql.CandidateEvent.Where(t => t.ReminderSent != true && t.Completed != true && t.Date > currentTime && t.Date < checkLimit).ToList();
                _logger.LogInformation("Found {Count} events for reminders in next 5 minutes.", eventList.Count);

                string message = string.Empty;

                foreach (CandidateEvent canEvent in eventList)
                {
                    canEvent.ReminderSent = true;
                    thql.SaveChanges();
                    Candidate candidate = thql.Candidate.Single(t => t.id == canEvent.CandidateId);
                    try
                    {
                        CandidatesVacancyStatu candidatesVacancy = thql.CandidatesVacancyStatus.Where(t => t.CandidateId == candidate.id && t.VacancyStatusId != 9).OrderByDescending(t => t.DateAdded).FirstOrDefault();
                        if (candidatesVacancy != null)
                        {
                            CandidatesVacancyStatu newConnect = new CandidatesVacancyStatu()
                            {
                                CandidateId = candidatesVacancy.CandidateId,
                                DateAdded = DateTime.UtcNow,
                                Note = "Automatic due to event",
                                VacancyId = candidatesVacancy.VacancyId,
                                VacancyStatusId = 11,
                                CreatedBy = 18
                            };
                            canEvent.HashCode = Guid.NewGuid().ToString();
                            thql.CandidatesVacancyStatus.Add(newConnect);
                            thql.SaveChanges();
                            _logger.LogInformation("Created new CandidatesVacancyStatu for candidate {CandidateId}.", candidate.id);
                        }
                        Vacancy candidateLastVacancy = candidatesVacancy != null ? thql.Vacancy.SingleOrDefault(t => t.id == candidatesVacancy.VacancyId) : null;
                        DateTime eventTime = TimeZoneInfo.ConvertTimeFromUtc(canEvent.Date, TZConvert.GetTimeZoneInfo(TZConvert.IanaToWindows("Europe/Moscow")));
                        message = String.Format("{0}\n{1}-{2}\n{3}\n{4}\n{5}/Vacancy/profile/0/Candidates/0/{6}\n{7}\n", eventTime.ToShortDateString(),
                            eventTime.ToString("HH:mm"),
                            eventTime.AddMinutes(45).ToString("HH:mm"), candidate.Name, candidateLastVacancy != null ? candidateLastVacancy.Name : "",
                            applicationDomainUrl,
                            candidate.id, canEvent.ZoomLink != null ? canEvent.ZoomLink : "");
                        if (canEvent.HashCode != null && candidateLastVacancy != null)
                        {
                            message = message + $"Link for changing status - {applicationDomainUrl}/Telegram/ChangeStatus/" + canEvent.HashCode;
                        }

                        if (thql.CandidateFile.Any(t => t.candidateId == canEvent.CandidateId))
                        {
                            List<CandidateFile> filesList = thql.CandidateFile.Where(t => t.candidateId == canEvent.CandidateId).ToList();
                            _logger.LogInformation("Sending reminder with files for candidate {CandidateId}. Message: {Message}", candidate.id, message);
                            TelegramBotReminder.telegramBotClient.sendReminderAll(message, filesList).Wait();
                        }
                        else
                        {
                            _logger.LogInformation("Sending reminder without files for candidate {CandidateId}. Message: {Message}", candidate.id, message);
                            TelegramBotReminder.telegramBotClient.sendReminderAll(message).Wait();
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error sending reminder for candidate {CandidateId}. EventId: {EventId}, EventDate: {EventDate}, CandidateName: {CandidateName}, Message: {Message}", candidate.id, canEvent.id, canEvent.Date, candidate.Name, message);
                        thql.Logs.Add(new Log()
                        {
                            Date = DateTime.UtcNow,
                            Text = $"ReminderCheck: CandidateId={candidate.id}, EventId={canEvent.id}, EventDate={canEvent.Date}, CandidateName={candidate.Name}, Message={message}\nError: {ex}"
                        });
                        thql.SaveChanges();
                    }
                }

                DateTime checkEarlyLimit = DateTime.UtcNow.AddMinutes(30);
                currentTime = DateTime.UtcNow;
                eventList = thql.CandidateEvent.Where(t => t.ReminderEarlySent != true && t.Completed != true && t.Date > currentTime && t.Date < checkEarlyLimit).ToList();
                _logger.LogInformation("Found {Count} events for early reminders in next 30 minutes.", eventList.Count);

                foreach (CandidateEvent canEvent in eventList)
                {
                    canEvent.ReminderEarlySent = true;
                    thql.SaveChanges();
                    Candidate candidate = thql.Candidate.Single(t => t.id == canEvent.CandidateId);
                    try
                    {
                        CandidatesVacancyStatu candidatesVacancy = thql.CandidatesVacancyStatus.Where(t => t.CandidateId == candidate.id && t.VacancyStatusId != 9).OrderByDescending(t => t.DateAdded).FirstOrDefault();
                        Vacancy candidateLastVacancy = candidatesVacancy != null ? thql.Vacancy.SingleOrDefault(t => t.id == candidatesVacancy.VacancyId) : null;
                        DateTime eventTime = TimeZoneInfo.ConvertTimeFromUtc(canEvent.Date, TZConvert.GetTimeZoneInfo(TZConvert.IanaToWindows("Europe/Moscow")));
                        message = String.Format("{0}\n{1}-{2}\n{3}\n{4}\n{5}/Vacancy/profile/0/Candidates/0/{6}\n{7}\n", eventTime.ToShortDateString(),
                            eventTime.ToString("HH:mm"),
                            eventTime.AddMinutes(45).ToString("HH:mm"), candidate.Name, candidateLastVacancy != null ? candidateLastVacancy.Name : "",
                            applicationDomainUrl,
                            candidate.id, canEvent.ZoomLink != null ? canEvent.ZoomLink : "");
                        if (canEvent.HashCode != null && candidateLastVacancy != null)
                        {
                            message = message + $"Link for changing status - {applicationDomainUrl}/Telegram/ChangeStatus/" + canEvent.HashCode;
                        }

                        if (thql.CandidateFile.Any(t => t.candidateId == canEvent.CandidateId))
                        {
                            List<CandidateFile> filesList = thql.CandidateFile.Where(t => t.candidateId == canEvent.CandidateId).ToList();
                            _logger.LogInformation("Sending early reminder with files for candidate {CandidateId}. Message: {Message}", candidate.id, message);
                            TelegramBotReminder.telegramBotClient.sendReminderAllEarlyBirds(message, filesList).Wait();
                        }
                        else
                        {
                            _logger.LogInformation("Sending early reminder without files for candidate {CandidateId}. Message: {Message}", candidate.id, message);
                            TelegramBotReminder.telegramBotClient.sendReminderAllEarlyBirds(message).Wait();
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error sending early reminder for candidate {CandidateId}. EventId: {EventId}, EventDate: {EventDate}, CandidateName: {CandidateName}, Message: {Message}", candidate.id, canEvent.id, canEvent.Date, candidate.Name, message);
                        thql.Logs.Add(new Log()
                        {
                            Date = DateTime.UtcNow,
                            Text = $"ReminderCheck: CandidateId={candidate.id}, EventId={canEvent.id}, EventDate={canEvent.Date}, CandidateName={candidate.Name}, Message={message}\nError: {ex}"
                        });
                        thql.SaveChanges();
                    }
                }
            }
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("TelegramReminderService is stopping.");
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
