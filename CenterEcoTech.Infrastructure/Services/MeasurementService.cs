using CenterEcoTech.Domain.DTO.MeasurementRequest;
using CenterEcoTech.Domain.Exeptions;
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
            var counter = _context.Counter
                .FirstOrDefault(x => x.ClientId == userId && x.Name == query.Name);

            var newMeasure = new Measurement()
            {
                Value = query.Value,
                Date = DateTime.Today,
                Counter = counter
            };

            await _context.Measurement.AddAsync(newMeasure, ct);
            //counter.Postfix = query.Value.ToString();
            await _context.SaveChangesAsync(ct);



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
            List<MeasurementRequestDto> metrics = new();

            var client = await _context.Client
                .Include(x => x.Adress)
                .Include(x => x.Counters)
                .FirstOrDefaultAsync(x => x.Id == userId, ct);

            if (client == null) throw new ApiException("client not found");

            var totalCounters = client.Counters;

            if (query.Type != null)
                totalCounters = totalCounters.Where(x => x.Name == query.Type).ToList();

            foreach (var r in totalCounters)
            {
                var queryM = _context.Entry(r)
                    .Collection(x => x.Measurements)
                    .Query();

                if (query.Date != null)
                    queryM = queryM.Where(x => x.Date == query.Date);

                var tempoMetrics = await queryM
                   .Select(x => new MeasurementRequestDto()
                   {
                       Date = x.Date,
                       Name = r.Name,
                       Value = x.Value,
                       Postfix = r.Postfix,
                       CLientName = client.GetFullName(),
                       CLientAdress = client.GetFullAdress(),
                   })
                   .ToListAsync(ct);

                metrics.AddRange(tempoMetrics);
            }

            return metrics;
        }

        public async Task<IEnumerable<LastCounterDto>> GetLastMeasurement(int userId, CancellationToken ct)
        {
            var client = await _context.Client
                .Include(x => x.Counters)
                .ThenInclude(x => x.Measurements)
                .FirstOrDefaultAsync(x => x.Id == userId, ct);
            if (client == null) throw new ApiException("client not found");

            var lastMeasurement = client.Counters
                .Select(x => new LastCounterDto(x.Name, x.Measurements.OrderBy(x => x.Date).First().Value))
                .ToList();

            return lastMeasurement;
        }

        public async Task AddNewCounter(AddCounterQuery query, int userId, CancellationToken ct)
        {
            var client = await _context.Client
                .FirstOrDefaultAsync(x => x.Id == userId, ct);
            if (client == null) throw new ApiException("client not found");

            var checkCounter = await _context.Counter
                .FirstOrDefaultAsync(x => x.ClientId == userId && x.Name.ToLower().Trim().Equals(query.Name.ToLower().Trim()), ct);
            if (checkCounter != null) throw new ApiException("counter already exist");

            var newRequest = new Counter
            {
                Name = query.Name,
                Client = client
            };

            await _context.Counter.AddAsync(newRequest, ct);
            await _context.SaveChangesAsync(ct);
        }
    }
}
