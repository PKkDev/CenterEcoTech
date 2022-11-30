using CenterEcoTech.Domain.DTO;
using CenterEcoTech.Domain.DTO.User;
using CenterEcoTech.Domain.Exeptions;
using CenterEcoTech.Domain.Query;
using CenterEcoTech.Domain.ServicesContract;
using CenterEcoTech.EfData.Context;
using CenterEcoTech.EfData.Entities;
using CenterEcoTech.Infrastructure.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Numerics;

namespace CenterEcoTech.Infrastructure.Services
{
    public class ClientService : IClientService
    {
        private AppDataBaseContext _context;
        private readonly IJWTGenerator _jwtTokenService;
        private readonly IHttpContextAccessor _accessor;
        private readonly IHttpClientFactory _clientFactory;

        private readonly string _sessionKeyCode = "_Code";

        public ClientService(
            AppDataBaseContext context, IJWTGenerator jwtGenerator, IHttpContextAccessor accessor, IHttpClientFactory clientFactory)
        {
            _context = context;
            _jwtTokenService = jwtGenerator;
            _accessor = accessor;
            _clientFactory = clientFactory;
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
                CooperativeId = query.СooperativeId,
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
            var client = _clientFactory.CreateClient("smsAreaApi");
            var message = $"код для доступа: {code}";
            Dictionary<string, string> queryParam = new()
            {
                {"number", $"{phone}"},
                {"text", $"{message}"},
                {"sign", "SMS Aero"}
            };
            var uri = QueryHelpers.AddQueryString(client.BaseAddress.AbsoluteUri, queryParam);
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var response = await client.SendAsync(request, ct);
            var responseMessage = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
                throw new ApiException("ошибка при отправке sms");
            //  _accessor.HttpContext.Session.SetString(_sessionKeyCode, code);
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

            //  var savedCode = _accessor.HttpContext.Session.GetString(_sessionKeyCode);

            // TODO
            //if (!savedCode.Equals(code)) throw new ApiException("wrong code");

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
        /// get client detail
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        /// <exception cref="ApiException"></exception>
        public async Task<UserDetailDto> GetClientDetailAsync(int userId, CancellationToken ct)
        {
            var client = await _context.Client
                .Include(x => x.Adress)
                .FirstOrDefaultAsync(x => x.Id == userId, ct);

            if (client == null)
                throw new ApiException("user not found");

            UserDetailDto result = new()
            {
                Phone = client.Phone,
                Email = client.Email,
                FirstName = client.FirstName,
                LastNme = client.LastNme,
                MidName = client.MidName,
                Adress = new()
                {
                    City = client.Adress.City,
                    Street = client.Adress.Street,
                    House = client.Adress.House,
                    Corpus = client.Adress.Corpus,
                    Room = client.Adress.Room,
                }
            };
            return result;
        }

        /// <summary>
        /// update client detail
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="detail"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        /// <exception cref="ApiException"></exception>
        public async Task UpdateClientDetailAsync(int userId, UserDetailDto detail, CancellationToken ct)
        {
            var client = await _context.Client
                .Include(x => x.Adress)
                .FirstOrDefaultAsync(x => x.Id == userId, ct);

            if (client == null)
                throw new ApiException("user not found");

            client.Phone = detail.Phone;
            client.Email = detail.Email;
            client.FirstName = detail.FirstName;
            client.LastNme = detail.LastNme;
            client.MidName = detail.MidName;

            client.Adress.City = detail.Adress.City;
            client.Adress.Street = detail.Adress.Street;
            client.Adress.House = detail.Adress.House;
            client.Adress.Corpus = detail.Adress.Corpus;
            client.Adress.Room = detail.Adress.Room;

            _context.Client.Update(client);
            await _context.SaveChangesAsync(ct);
        }

        /// <summary>
        /// delete client
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        /// <exception cref="ApiException"></exception>
        public async Task DeleteClientAsync(int userId, CancellationToken ct)
        {
            var client = await _context.Client
                .FirstOrDefaultAsync(x => x.Id == userId, ct);

            if (client == null)
                throw new ApiException("user not found");

            _context.Client.Remove(client);
            await _context.SaveChangesAsync(ct);
        }
    }
}
