using CenterEcoTech.Domain.DTO;
using CenterEcoTech.Domain.DTO.User;
using CenterEcoTech.Domain.Query;
using CenterEcoTech.Domain.ServicesContract;
using CenterEcoTech.EfData.Entities;
using CenterEcoTech.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;


namespace CenterEcoTech.API.Controllers
{
    [Route("api/Client")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IOptions<JWTGenerator> _jwtOptions;
        private readonly IClient _client;
        private readonly IHttpContextAccessor _accessor;

        public ClientController(IClient iClient, IOptions<JWTGenerator> jwtOptions, IHttpContextAccessor accessor)
        {
            _jwtOptions = jwtOptions;
            _client = iClient;
            _accessor = accessor;
        }     
        
        [HttpPost("register")]
        public IActionResult Create([FromBody] RegisterQuery query)
        {
            
            if (query == null)
            {
                return BadRequest();
            }
            _client.Create(query);
            return Ok("created!");
        }        

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
        {
            await _client.SendAccesTokenToSmsAsync(query.Phone, ct);
        }

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
        {
            return await _client.CheckPhoneAccessTokenAsync(query.Phone, query.Code, ct);
        }

        /// <summary>
        /// test check auth
        /// </summary>
        /// <returns></returns>
        [HttpGet("check-auth")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult CheckAuth() => Ok("all ok!");


        /// <summary>
        /// get user detail
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpGet("detail")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<UserDetailDto> GetUserDetail(CancellationToken ct = default)
        {
            var userId = this.User.Claims.First().Value;
            return await _client.GetUserDetailAsync(Convert.ToInt32(userId), ct);
        }



    }
}
