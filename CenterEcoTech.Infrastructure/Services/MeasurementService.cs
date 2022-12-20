using CenterEcoTech.Domain.DTO.MeasurementRequest;
using CenterEcoTech.Domain.Exeptions;
using CenterEcoTech.Domain.Query.ClientRequest;
using CenterEcoTech.Domain.Query.MeasurementRequest;
using CenterEcoTech.Domain.ServicesContract;
using CenterEcoTech.EfData.Context;
using CenterEcoTech.EfData.Entities;
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

		public async Task AddMeasurementAsync(AddMeasurementQuery query, int userId, CancellationToken ct)
		{
			var client = await _context.Client
				.Include(x => x.Adress)
				.FirstOrDefaultAsync(x => x.Id == userId, ct);
			if (client == null) throw new ApiException("client not found");

			throw new NotImplementedException();
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

            //var queryR = _context.Entry(client).Collection(x => x.Measurements).Query();
/*
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
                .ToListAsync(ct);*/

            return new List<MeasurementRequestDto>();
        }

        public async Task<IEnumerable<LastCounterDto>> GetLastMeasurement(LastCounterDto query, int userId, CancellationToken ct)
        {
			var client = await _context.Client
				.Include(x => x.Adress)
				.FirstOrDefaultAsync(x => x.Id == userId, ct);
			if (client == null) throw new ApiException("client not found");

            var counteers = client.Counters.Where(x => x.Id == userId).ToList();

            var lastMeasurement = counteers
                .Select(x=> new LastCounterDto()
                {
                    Name = counteers.First().Name,
                    Value = counteers.First().Measurements.OrderBy(x=> x.Date).First().Value
                    
                }).ToList();

            return lastMeasurement;
			throw new NotImplementedException();
        }


        public async Task AddNewCounter(AddCounterQuery query, int userId, CancellationToken ct)
        {

            var client = await _context.Client
                .FirstOrDefaultAsync(x => x.Id == userId, ct);
            if (client == null) throw new ApiException("client not found");

            var newRequest = new Counter
			{ 
                Name = query.Name
            };

            await _context.Counter.AddAsync(newRequest, ct);
            await _context.SaveChangesAsync(ct);
        }
	}
}
