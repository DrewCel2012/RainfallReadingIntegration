using Microsoft.AspNetCore.Mvc;
using RainfallReading.Model;
using RainfallReading.Service.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace RainfallReading.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RainfallReadingController : ControllerBase
    {
        private readonly IRainfallReadingService _rainfallReadingService;

        public RainfallReadingController(IRainfallReadingService rainfallReadingService)
        {
            _rainfallReadingService = rainfallReadingService;
        }


        /// <summary>
        /// Get rainfall readings by station Id
        /// </summary>
        /// <param name="stationId">The id of the reading station</param>
        /// <param name="count">The number of readings to return</param>
        /// <response code="200">A list of rainfall readings successfully retrieved</response>
        /// <response code="400">Invalid request</response>
        /// <response code="404">No readings found for the specified stationId</response>
        /// <response code="500">Internal server error</response>
        [HttpGet]
        [Route("/rainfall/id/{stationId:minlength(6):maxlength(10)}/readings")]
        [ProducesResponseType(typeof(RainfallReadingResponse), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        [ProducesResponseType(typeof(Error), 404)]
        [ProducesResponseType(typeof(Error), 500)]
        [Produces("application/json")]
        public async Task<ActionResult<RainfallReadingResponse>> Get(string stationId, [FromQuery, Range(1, 100)] int? count = 10)
        {
            var result = await _rainfallReadingService.GetRainfallReadingsAsync(stationId, count);

            return result;
        }
    }
}
