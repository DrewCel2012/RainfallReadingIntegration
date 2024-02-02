using RainfallReading.Model;

namespace RainfallReading.Service.Interfaces
{
    public interface IRainfallReadingService
    {
        Task<RainfallReadingResponse> GetRainfallReadingsAsync(string stationId, int? count = 10);
    }
}
