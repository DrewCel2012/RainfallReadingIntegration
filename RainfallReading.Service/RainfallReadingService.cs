using AutoMapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RainfallReading.Model;
using RainfallReading.Service.DomainModels;
using RainfallReading.Service.Interfaces;
using RainfallReading.Service.MapperConfigs;

namespace RainfallReading.Service
{
    public class RainfallReadingService : IRainfallReadingService
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMapper _mapper = MapperConfig.InitializeRainfallReadingMapper;

        private readonly JsonSerializerSettings serializerSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            DateParseHandling = DateParseHandling.DateTime
        };


        public RainfallReadingService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;

            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
        }


        public async Task<RainfallReadingResponse> GetRainfallReadingsAsync(string stationId, int? count = 10)
        {
            var url = $"id/stations/{stationId}/readings?_sorted&_limit={count}";
            var httpClient = _httpClientFactory.CreateClient(_configuration["Rainfall.Api:ClientName"]);

            string content = string.Empty;
            var response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
                content = await response.Content.ReadAsStringAsync();

            var readings = JsonConvert.DeserializeObject<FloodMonitoringReadingsDomain>(content, serializerSettings) ?? new();

            return _mapper.Map<RainfallReadingResponse>(readings);
        }
    }
}
