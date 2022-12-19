using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CenterEcoTech.Domain.DTO;
using CenterEcoTech.Domain.DTO.User;
using CenterEcoTech.Domain.Query;
using CenterEcoTech.Domain.ServicesContract;
using CenterEcoTech.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using CenterEcoTech.Infrastructure.Services;

namespace CenterEcoTech.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MeasurementController : ControllerBase
	{
		private readonly IMeasurementService _measurementService;

		public MeasurementController(IMeasurementService measurementService)
		{
			_measurementService = measurementService;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="ct"></param>
		/// <returns></returns>
		[HttpGet("history")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task GetHistory(CancellationToken ct = default)
		{
			var userId = HttpContext.GetClientId();
			await _measurementService.GetHistoryMeasurement(userId, ct);
		}
	}
}
