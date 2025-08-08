using Microsoft.Extensions.Options;
using Oqtacore.Rrm.Application.Configs;
using System.Text;
using System.Text.Json;

namespace Oqtacore.Rrm.Application.ServiceAgent
{
    public class PlatrumApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly PlatrumMetricsSettings _platrumMetricsSettings;
        public PlatrumApiClient(HttpClient httpClient, IOptions<PlatrumMetricsSettings> platrumMetricsSettings)
        {
            _httpClient = httpClient;
            _platrumMetricsSettings = platrumMetricsSettings.Value;
        }
        public async Task<HttpResponseMessage> SendToPlatrum400(int count)
        {
            return await SendToPlatrumAsync(_platrumMetricsSettings.ClientErrorsMetricId, _platrumMetricsSettings.PlatrumUserId, count);
        }
        public async Task<HttpResponseMessage> SendToPlatrum500(int count)
        {
            return await SendToPlatrumAsync(_platrumMetricsSettings.ServerErrorsMetricId, _platrumMetricsSettings.PlatrumUserId, count);
        }
        private async Task<HttpResponseMessage> SendToPlatrumAsync(string metricId, string userId, int count)
        {
            var currentDate = DateTime.UtcNow;
            var yesterday = currentDate.AddDays(-1);

            var data = new
            {
                kpi_id = metricId,
                user_id = userId,
                end_period_date = yesterday.ToString("yyyy-MM-dd"),
                value = count
            };

            var jsonData = JsonSerializer.Serialize(data);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Api-key", _platrumMetricsSettings.PlatrumApiKey);

            Console.WriteLine(data);
            return await _httpClient.PostAsync("api/kpi/data/save", content);
        }
    }
}
