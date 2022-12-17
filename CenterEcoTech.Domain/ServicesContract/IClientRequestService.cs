using CenterEcoTech.Domain.DTO.ClientRequest;
using CenterEcoTech.Domain.Query.ClientRequest;

namespace CenterEcoTech.Domain.ServicesContract
{
    public interface IClientRequestService
    {
        Task<IEnumerable<ClientRequestsDto>> GetClientRequestsAsync(GetClientRequestHistoryQuery query, int clientId, CancellationToken ct);

        Task AddClientRequestAsync(AddClientRequestQuery query, int clientId, CancellationToken ct);
    }
}
