using CenterEcoTech.Domain.DTO.MeasurementRequest;
using CenterEcoTech.Domain.DTO.User;
using CenterEcoTech.Domain.Exeptions;
using CenterEcoTech.Domain.ServicesContract;
using CenterEcoTech.EfData.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenterEcoTech.Infrastructure.Services
{
	public class MeasurementService
	{
		private AppDataBaseContext _context;
		private readonly IJWTGenerator _jwtTokenService;
		private readonly IHttpContextAccessor _accessor;
		private readonly ISmsAeroService _smsAeroService;

		private readonly string _sessionKeyCode = "_Code";

		public MeasurementService(
			AppDataBaseContext context, IJWTGenerator jwtGenerator,
			IHttpContextAccessor accessor, ISmsAeroService smsAeroService)
		{
			_context = context;
			_jwtTokenService = jwtGenerator;
			_accessor = accessor;
			_smsAeroService = smsAeroService;
		}

		/// <summary>
		/// get mesuarement history
		/// </summary>
		/// <param name="userId"></param>
		/// <param name="ct"></param>
		/// <returns></returns>
		/// <exception cref="ApiException"></exception>
		public async Task<MeasurementRequestDto> GetHistotyMeasurement(int userId, CancellationToken ct)
		{
			var client = await _context.Client
				.Include(x => x.Adress)
				.FirstOrDefaultAsync(x => x.Id == userId, ct);

			if (client == null)
				throw new ApiException("user not found");

			var measurement = _context.Measurement
				.Where(x => x.Id == userId)
				.FirstOrDefault();

			if (measurement == null)
				throw new ApiException("measurement not found");

			MeasurementRequestDto result = new()
			{
				Date = measurement.Date,
				Name = measurement.Name,
				Value = measurement.Value,
				CLientName = client.GetFullName(),
				CLientAdress = client.GetFullAdress(),
			};

			return result;
		}
	}
}
