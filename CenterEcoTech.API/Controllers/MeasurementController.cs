using Microsoft.AspNetCore.Mvc;
using CenterEcoTech.Domain.ServicesContract;
using CenterEcoTech.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using CenterEcoTech.Domain.DTO.MeasurementRequest;
using CenterEcoTech.Domain.Query.MeasurementRequest;

namespace CenterEcoTech.API.Controllers
{
    [Route("api/measurement]")]
    [ApiController]
    public class MeasurementController : ControllerBase
    {
        private readonly IMeasurementService _measurementService;

        public MeasurementController(IMeasurementService measurementService)
        {
            _measurementService = measurementService;
        }

        /// <summary>
        /// get measurement history
        /// </summary>
        /// <param name="query"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpGet("history")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IEnumerable<MeasurementRequestDto>> GetHistory(
           [FromBody] GetMeasurementHistoryQuery query, CancellationToken ct = default)
        {
            var userId = HttpContext.GetClientId();
            return await _measurementService.GetHistoryMeasurement(query, userId, ct);
        }
    }
}
