using CenterEcoTech.Domain.DTO;
using CenterEcoTech.Domain.DTO.User;
using CenterEcoTech.Domain.Exeptions;
using CenterEcoTech.Domain.Query;
using CenterEcoTech.Domain.ServicesContract;
using CenterEcoTech.EfData.Context;
using CenterEcoTech.EfData.Entities;
using CenterEcoTech.Infrastructure.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CenterEcoTech.Infrastructure.Services
{
    public class ClientService : IClientService
    {
        private AppDataBaseContext _context;
        private readonly IJWTGenerator _jwtTokenService;
        private readonly IHttpContextAccessor _accessor;

        private readonly string _sessionKeyCode = "_Code";

        public ClientService(
            AppDataBaseContext context, IJWTGenerator jwtGenerator, IHttpContextAccessor accessor)
        {
            _context = context;
            _jwtTokenService = jwtGenerator;
            _accessor = accessor;
        }

        /// <summary>
        /// register user
        /// </summary>
        /// <param name="query"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task RegisterUserAsync(RegisterQuery query, CancellationToken ct)
        {
            var user = new Client()
            {
                FirstName = query.FirstName,
                LastNme = query.LastNme,
                MidName = query.MidName,
                Phone = query.Phone,
                Email = query.Email,
                СooperativeId = query.СooperativeId,
                Adress = new ClientAdress()
                {
                    City = query.Adress.City,
                    Street = query.Adress.Street,
                    House = query.Adress.House,
                    Corpus = query.Adress.Corpus,
                    Room = query.Adress.Room,
                }
            };

            await _context.Client.AddAsync(user, ct);
            await _context.SaveChangesAsync(ct);
        }

        /// <summary>
        /// send sms with code to user
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        /// <exception cref="ApiException"></exception>
        public async Task SendAccesTokenToSmsAsync(string phone, CancellationToken ct)
        {
            var user = await _context.Client
                .FirstOrDefaultAsync(x => x.Phone.Equals(phone), ct);

            if (user == null)
                throw new ApiException("user not found");

            var code = GeneratePhoneNumberToken();
            _accessor.HttpContext.Session.SetString(_sessionKeyCode, code);
        }

        /// <summary>
        /// check code from sms and user
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="code"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        /// <exception cref="ApiException"></exception>
        public async Task<LoginResponseDto> CheckPhoneAccessTokenAsync(
            string phone, string code, CancellationToken ct)
        {
            var user = await _context.Client
               .FirstOrDefaultAsync(x => x.Phone.Equals(phone), ct);

            if (user == null)
                throw new ApiException("user not found");

            var savedCode = _accessor.HttpContext.Session.GetString(_sessionKeyCode);

            if (!savedCode.Equals(code))
                throw new ApiException("wrong code");

            return await Authorize(user.Id, ct);
        }

        /// <summary>
        /// authentication user on login/pass
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private async Task<LoginResponseDto> Authorize(int id, CancellationToken ct)
        {
            string token = await _jwtTokenService.CreateTokenAsync(id, ct);
            return new LoginResponseDto(token);
        }

        /// <summary>
        /// generate 4-digit code
        /// </summary>
        /// <returns></returns>
        private string GeneratePhoneNumberToken()
        {
            var rnd = new Random();
            var start = rnd.Next(9, 99);
            var end = DateTime.Now.Second;
            return start.ToString() + end.ToString();
        }

        /// <summary>
        /// get user detail
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        /// <exception cref="ApiException"></exception>
        public async Task<UserDetailDto> GetUserDetailAsync(int userId, CancellationToken ct)
        {
            var user = await _context.Client
                .FirstOrDefaultAsync(x => x.Id == userId, ct);

            if (user == null)
                throw new ApiException("user not found");

            UserDetailDto result = new()
            {
                Phone = user.Phone,
                Email = user.Email,
                FirstName = user.FirstName
            };
            return result;
        }
    }
}
