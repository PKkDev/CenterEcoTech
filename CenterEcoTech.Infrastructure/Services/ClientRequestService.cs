using CenterEcoTech.Domain.DTO.ClientRequest;
using CenterEcoTech.Domain.Exeptions;
using CenterEcoTech.Domain.Query.ClientRequest;
using CenterEcoTech.Domain.ServicesContract;
using CenterEcoTech.EfData.Context;
using CenterEcoTech.EfData.Entities;
using Microsoft.EntityFrameworkCore;

namespace CenterEcoTech.Infrastructure.Services
{
    public class ClientRequestService : IClientRequestService
    {
        private AppDataBaseContext _context;

        public ClientRequestService(AppDataBaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ClientRequestsDto>> GetClientRequestsAsync(
            GetClientRequestHistoryQuery query, int clientId, CancellationToken ct)
        {
            var client = await _context.Client
                .Include(x => x.Adress)
                .FirstOrDefaultAsync(x => x.Id == clientId, ct);
            if (client == null) throw new ApiException("client not found");

            var queryR = _context.Entry(client).Collection(x => x.Requests).Query();

            if (query.Date != null)
                queryR = queryR.Where(x => x.Date == query.Date);

            if (query.Status != null)
                queryR = queryR.Where(x => x.Status == query.Status);

            var metrics = await queryR
                .Select(x => new ClientRequestsDto()
                {
                    Date = x.Date,
                    Theme = x.Theme,
                    Message = x.Message,
                    Status = x.Status,
                    CLientName = client.GetFullName(),
                    CLientAdress = client.GetFullAdress()
                })
                .ToListAsync(ct);
            return metrics;
        }

        public async Task AddClientRequestAsync(
            AddClientRequestQuery query, int clientId, CancellationToken ct)
        {
            var client = await _context.Client
                .FirstOrDefaultAsync(x => x.Id == clientId, ct);
            if (client == null) throw new ApiException("client not found");

            var today = DateTime.Today;
            var newRequest = new Request()
            {
                Client = client,
                Message = query.Message,
                Theme = query.Theme,
                Status = RequestStatus.New,
                Date = DateTime.Today
            };

            await _context.Request.AddAsync(newRequest, ct);
            await _context.SaveChangesAsync(ct);
        }

    }
}
