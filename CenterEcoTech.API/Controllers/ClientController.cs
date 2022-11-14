using CenterEcoTech.Domain.DTO;
using CenterEcoTech.Domain.Query;
using CenterEcoTech.EfData.Entities;
using CenterEcoTech.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Security.Principal;
using System.Xml.Linq;

namespace CenterEcoTech.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IOptions<JWTGenerator> jwtOptions;
        private readonly IClient iClient;
        IHttpContextAccessor accessor;

        public ClientController(IClient iClient, IOptions<JWTGenerator> jwtOptions, IHttpContextAccessor accessor)
        {
            this.jwtOptions = jwtOptions;
            this.iClient = iClient;
            this.accessor = accessor;
        }

        [HttpGet(Name = "GetAllItems")]
        public IEnumerable<Client> Get()
        {
            return iClient.Get();
        }


        [HttpGet("{id}", Name = "GetTodoItem")]
        public IActionResult Get(int Id)
        {
            Client todoItem = iClient.Get(Id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return new ObjectResult(todoItem);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Client todoItem)
        {
            if (todoItem == null)
            {
                return BadRequest();
            }
            iClient.Create(todoItem);
            return CreatedAtRoute("GetTodoItem", new { id = todoItem.Id }, todoItem);
        }

        
        [HttpDelete("{id}")]
        public IActionResult Delete(int Id)
        {
            var deletedTodoItem = iClient.Delete(Id);

            if (deletedTodoItem == null)
            {
                return BadRequest();
            }

            return new ObjectResult(deletedTodoItem);
        }

        /// <summary>
        /// send sms with code to user
        /// </summary>
        /// <param name="query"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("send-sms")]
        public async Task SendAccesTokenToSms(
            [FromBody] PhoneAuthorizeQuery query, CancellationToken ct = default)
        {
            await iClient.SendAccesTokenToSmsAsync(query.Phone, ct);
        }

        /// <summary>
        /// check code from sms and user
        /// </summary>
        /// <param name="query"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("check-sms")]
        public async Task<LoginResponseDto> CheckPhoneAccessToken(
            [FromBody] CheckPhoneAuthorizeQuery query, CancellationToken ct = default)
        {
            return await iClient.CheckPhoneAccessTokenAsync(query.Phone, query.Code, ct);
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
            return await iClient.GetUserDetailAsync(Convert.ToInt32(userId), ct);
        }



    }
}
