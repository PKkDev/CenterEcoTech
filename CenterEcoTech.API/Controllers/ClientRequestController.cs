using CenterEcoTech.Domain.DTO.ClientRequest;
using CenterEcoTech.Domain.Query.ClientRequest;
using CenterEcoTech.Domain.ServicesContract;
using CenterEcoTech.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CenterEcoTech.API.Controllers
{
    [Route("api/client-request")]
    [ApiController]
    public class ClientRequestController : ControllerBase
    {
        private readonly IClientRequestService _clientRequestService;

        public ClientRequestController(IClientRequestService clientRequestService)
        {
            _clientRequestService = clientRequestService;
        }

        [HttpPost("history")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IEnumerable<ClientRequestsDto>> GetClientRequests(
            [FromBody] GetClientRequestHistoryQuery query, CancellationToken ct = default)
        {
            var clientId = HttpContext.GetClientId();
            return await _clientRequestService.GetClientRequestsAsync(query, clientId, ct);
        }

        [HttpPost("add-request")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task AddClientRequest(
            [FromBody] AddClientRequestQuery query, CancellationToken ct = default)
        {
            var clientId = HttpContext.GetClientId();
            await _clientRequestService.AddClientRequestAsync(query, clientId, ct);
        }
    }
}
