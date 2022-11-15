﻿using CenterEcoTech.Domain.DTO;
using CenterEcoTech.EfData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using CenterEcoTech.Domain.Query;

namespace CenterEcoTech.Infrastructure.Services
{
    public interface IClient
    {       

        IEnumerable<Client> Get();
        Client Get(int id);
        void Create(RegisterQuery query);
        
        Client Delete(int id);
        /// <summary>
        /// send sms with code to user
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task SendAccesTokenToSmsAsync(string phone, CancellationToken ct);

        /// <summary>
        /// check code from sms and user
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="code"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<LoginResponseDto> CheckPhoneAccessTokenAsync(string phone, string code, CancellationToken ct);

        /// <summary>
        /// authentication user on login/pass
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<LoginResponseDto> Authorize(Client user);
             

        /// <summary>
        /// get user detail
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<UserDetailDto> GetUserDetailAsync(int userId, CancellationToken ct);


    }
}
