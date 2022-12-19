using CenterEcoTech.Domain.DTO;
using CenterEcoTech.Domain.DTO.User;
using CenterEcoTech.Domain.Query;
using CenterEcoTech.Domain.ServicesContract;
using CenterEcoTech.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CenterEcoTech.API.Controllers
{
    [Route("api/client")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        /// <summary>
        /// register user
        /// </summary>
        /// <param name="query"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task RegisterUser([FromBody] RegisterQuery query, CancellationToken ct = default)
            => await _clientService.RegisterUserAsync(query, ct);

        /// <summary>
        /// send sms with code to user
        /// </summary>
        /// <param name="query"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("send-sms")]
        public async Task SendAccesTokenToSmsAsync(
            [FromBody] PhoneAuthorizeQuery query, CancellationToken ct = default)
            => await _clientService.SendAccesTokenToSmsAsync(query.Phone, ct);

        /// <summary>
        /// check code from sms and user
        /// </summary>
        /// <param name="query"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("check-sms")]
        public async Task<LoginResponseDto> CheckPhoneAccessTokenAsync(
            [FromBody] CheckPhoneAuthorizeQuery query, CancellationToken ct = default)
            => await _clientService.CheckPhoneAccessTokenAsync(query.Phone, query.Code, ct);

        /// <summary>
        /// test check auth
        /// </summary>
        /// <returns></returns>
        [HttpGet("check-auth")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult CheckAuth() => Ok("all ok!");

        /// <summary>
        /// get client detail
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpGet("detail")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<UserDetailDto> GetUserDetail(CancellationToken ct = default)
        {
            var userId = HttpContext.GetClientId();
            return await _clientService.GetClientDetailAsync(Convert.ToInt32(userId), ct);
        }

        /// <summary>
        /// update client detail
        /// </summary>
        /// <param name="query"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpPost("detail")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task UpdateUserDetail([FromBody] UserDetailDto query, CancellationToken ct = default)
        {
            var userId = HttpContext.GetClientId();
            await _clientService.UpdateClientDetailAsync(userId, query, ct);
        }

        /// <summary>
        /// delete client
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task DeleteUser(CancellationToken ct = default)
        {
            var userId = HttpContext.GetClientId();
            await _clientService.DeleteClientAsync(userId, ct);
        }
	}
}
