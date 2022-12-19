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
        /// add mesuarement 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="clientId"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        /// <exception cref="ApiException"></exception>
        public async Task AddMeasurementAsync(
            AddMeasurementQuery query, int clientId, CancellationToken ct)
        {
            var client = await _context.Client
                .FirstOrDefaultAsync(x => x.Id == clientId, ct);
            if (client == null) throw new ApiException("client not found");

            var today = DateTime.Today;
            var newMeasure = new Measurement()
            {
                Client = client,
                Name = query.Name,
                Value = query.Value,                
                Date = DateTime.Today
            };

            await _context.Measurement.AddAsync(newMeasure, ct);
            await _context.SaveChangesAsync(ct);
        }
    }
}
