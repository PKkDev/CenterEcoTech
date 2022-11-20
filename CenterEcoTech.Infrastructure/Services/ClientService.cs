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
    public class ClientService : IClient
    {
        private AppDataBaseContext _context;
        private readonly IJWTGenerator _jwtTokenService;
        public string SessionKeyCode = "_Code";
        IHttpContextAccessor _accessor;

        public string SessionInfo_Code { get; private set; }

        public IEnumerable<Client> Get()
        {           
            return _context.Client;
        }

        public Client Get(int Id)
        {
            return _context.Client.Find(Id);
        }

        public ClientService(AppDataBaseContext context, IJWTGenerator jwtGenerator, IHttpContextAccessor accessor)
        {
            _context = context;
            _jwtTokenService = jwtGenerator;
            _accessor = accessor;
        }

        /// <summary>
        /// send sms with code to user
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task SendAccesTokenToSmsAsync(string phone, CancellationToken ct)
        {
            var user = await _context.Client.FirstOrDefaultAsync(x => x.Phone.Equals(phone), ct);
               
            if (user == null)
            {
                Client newUser = new()
                {
                    Phone = phone                    
                };
                await _context.Client.AddAsync(newUser, ct);
                await _context.SaveChangesAsync(ct);

                user = await _context.Client
                    .FirstOrDefaultAsync(x => x.Phone.Equals(phone), ct);
            }

            var code = GeneratePhoneNumberTokenAsync();
            var httpContext = _accessor.HttpContext;
            httpContext.Session.SetString(SessionKeyCode, code);
        }

        /// <summary>
        /// check code from sms and user
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="code"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<LoginResponseDto> CheckPhoneAccessTokenAsync(
            string phone, string code, CancellationToken ct)
        {           
            var user = await _context.Client
               .FirstOrDefaultAsync(x => x.Phone.Equals(phone), ct);

            return await Authorize(user.Id);
        }

        /// <summary>
        /// authentication user on login/pass
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<LoginResponseDto> Authorize(int id)
        {
            string token = _jwtTokenService.CreateTokenAsync(id).Result;
            var result = new LoginResponseDto(token);
            return result;
        }

        /// <summary>
        /// generate 4-digit code
        /// </summary>
        /// <returns></returns>
        private string GeneratePhoneNumberTokenAsync()
        {
            var rnd = new Random();
            var start = rnd.Next(9, 99);
            var end = DateTime.Now.Second;
            return start.ToString() + end.ToString();
        }
                
        public void Create(RegisterQuery query)
        {
            
            var user = new Client() { FirstName = query.FirstName, LastNme= query.LastNme, MidName= query.MidName,
            Phone= query.Phone, Email= query.Email, СooperativeId= query.СooperativeId};
            _context.Client.Add(user);
            _context.SaveChanges();
            var adress = new ClientAdress() { City = query.Adress.City, Street= query.Adress.Street,
                House= query.Adress.House, Corpus= query.Adress.Corpus, Room= query.Adress.Room, ClientId=user.Id };
            _context.ClientAdress.Add(adress);
            _context.SaveChanges();
        }       

        public Client Delete(int Id)
        {
            Client client = Get(Id);

            if (client != null)
            {
                _context.Client.Remove(client);
                _context.SaveChanges();
            }

            return client;
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
                .FirstOrDefaultAsync(x => x.Id == userId);

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
