using CenterEcoTech.Domain.DTO;
using CenterEcoTech.EfData.Context;
using CenterEcoTech.EfData.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenterEcoTech.Infrastructure.Services
{
    public class ClientService : IClient
    {
        private AppDataBaseContext Context;
        private readonly IJWTGenerator _jwtTokenService;
        public string SessionKeyCode = "_Code";
        IHttpContextAccessor accessor;

        public string SessionInfo_Code { get; private set; }

        public IEnumerable<Client> Get()
        {
            return Context.Client;
        }
        public Client Get(int Id)
        {
            return Context.Client.Find(Id);
        }
        public ClientService(AppDataBaseContext context, IJWTGenerator jwtGenerator, IHttpContextAccessor accessor)
        {
            Context = context;
            _jwtTokenService = jwtGenerator;
            this.accessor = accessor;
        }
        /// <summary>
        /// send sms with code to user
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task SendAccesTokenToSmsAsync(string phone, CancellationToken ct)
        {
            var user = await Context.Client.FirstOrDefaultAsync(x => x.Phone.Equals(phone), ct);
               
            if (user == null)
            {
                Client newUser = new()
                {
                    Phone = phone                    
                };
                await Context.Client.AddAsync(newUser, ct);
                await Context.SaveChangesAsync(ct);

                user = await Context.Client
                    .FirstOrDefaultAsync(x => x.Phone.Equals(phone), ct);
            }

            var code = GeneratePhoneNumberTokenAsync();            
            var httpContext = accessor.HttpContext;
            //if (string.IsNullOrEmpty(httpContext.Session.GetString(SessionKeyCode)))
            //{
                httpContext.Session.SetString(SessionKeyCode, code);
            //}


            Context.Client.Update(user);
            await Context.SaveChangesAsync(ct);


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
           
            var user = await Context.Client
               .FirstOrDefaultAsync(x => x.Phone.Equals(phone), ct);



            return await Authorize(user);
        }


        /// <summary>
        /// authentication user on login/pass
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<LoginResponseDto> Authorize(
            Client user)
        {
            string token = _jwtTokenService.CreateToken(user);
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

        
        public void Create(Client item)
        {
            Context.Client.Add(item);
            Context.SaveChanges();
        }
       

        public Client Delete(int Id)
        {
            Client todoItem = Get(Id);

            if (todoItem != null)
            {
                Context.Client.Remove(todoItem);
                Context.SaveChanges();
            }

            return todoItem;
        }
        
    }
}
