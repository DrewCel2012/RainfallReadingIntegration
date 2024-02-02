using Moq;
using RainfallReading.Service;
using RainfallReading.Service.DomainModels;
using RainfallReading.Test.Helpers;
using RainfallReading.Test.MockDataObjects;

namespace RainfallReading.Test
{
    public class RainfallReadingServiceTest
    {
        private Mock<IHttpClientFactory> _mockHttpClientFactory;

        public RainfallReadingServiceTest()
        {
            _mockHttpClientFactory = new Mock<IHttpClientFactory>();
        }


        [Fact]
        public void GetAsync_ShouldReturnRainfallReadingResponseData()
        {
            ShouldReturnRainfallReadingResponseData().Wait();
        }


        private async Task ShouldReturnRainfallReadingResponseData()
        {
            //Arrange:
            var mockData = RainfallReadingMockData.GetTestData;
            var mockHandler = HttpClientHelper.GetResults<FloodMonitoringReadingsDomain>(mockData);
            var mockHttpClient = new HttpClient(mockHandler.Object)
            {
                BaseAddress = new Uri("http://environment.data.gov.uk/flood-monitoring/")
            };
            _mockHttpClientFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(mockHttpClient);

            var rainfallService = new RainfallReadingService(_mockHttpClientFactory.Object);

            //Act:
            var results = await rainfallService.GetRainfallReadingsAsync("");

            //Assert
            Assert.NotNull(results);
            Assert.True(results.Readings?.Count > 0);
        }
    }
}