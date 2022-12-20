using CenterEcoTech.Domain.DTO.MeasurementRequest;
using CenterEcoTech.Domain.Exeptions;
using CenterEcoTech.Domain.Query.MeasurementRequest;
using CenterEcoTech.Domain.ServicesContract;
using CenterEcoTech.EfData.Context;
using Microsoft.EntityFrameworkCore;

namespace CenterEcoTech.Infrastructure.Services
{
    public class MeasurementService : IMeasurementService
    {
        private readonly AppDataBaseContext _context;

        public MeasurementService(AppDataBaseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// get mesuarement history
        /// </summary>
        /// <param name="query"></param>
        /// <param name="userId"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        /// <exception cref="ApiException"></exception>
        public async Task<IEnumerable<MeasurementRequestDto>> GetHistoryMeasurement(
            GetMeasurementHistoryQuery query, int userId, CancellationToken ct)
        {
            var client = await _context.Client
                .Include(x => x.Adress)
                .FirstOrDefaultAsync(x => x.Id == userId, ct);
            if (client == null) throw new ApiException("client not found");

            var queryR = _context.Entry(client).Collection(x => x.Measurements).Query();

            if (query.Date != null)
                queryR = queryR.Where(x => x.Date == query.Date);

            if (query.Status != null)
                queryR = queryR.Where(x => x.Name == query.Status);

            var metrics = await queryR
                .Select(x => new MeasurementRequestDto()
                {
                    Date = x.Date,
                    Name = x.Name,
                    Value = x.Value,
                    CLientName = client.GetFullName(),
                    CLientAdress = client.GetFullAdress(),
                })
                .ToListAsync(ct);

            return metrics;
        }

        /// <summary>
        /// get last measurement
        /// </summary>
        /// <param name="query"></param>
        /// <param name="userId"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        /// <exception cref="ApiException"></exception>
        public async Task<IEnumerable<MeasurementRequestDto>> GetLastMeasurement(
			GetMeasurementHistoryQuery query, int userId, CancellationToken ct)
        {
			var client = await _context.Client
			   .Include(x => x.Adress)
			   .FirstOrDefaultAsync(x => x.Id == userId, ct);
			if (client == null) throw new ApiException("client not found");

			var queryR = _context.Entry(client).Collection(x => x.Measurements).Query();

			if (query.Date != null)
				queryR = queryR.Where(x => x.Date == query.Date);

			if (query.Status != null)
				queryR = queryR.Where(x => x.Name == query.Status);

			var metrics = await queryR
				.Select(x => new MeasurementRequestDto()
				{
					Date = x.Date,
					Name = x.Name,
					Value = x.Value,
					CLientName = client.GetFullName(),
					CLientAdress = client.GetFullAdress(),
				})
				.ToListAsync(ct);

			return metrics;
		}

        public async Task AddMeasutement(
            GetMeasurementHistoryQuery query, int userId, CancellationToken ct)
        {

        }
	}
}
