using CenterEcoTech.Domain.DTO;
using CenterEcoTech.Domain.DTO.User;
using CenterEcoTech.Domain.Query;

namespace CenterEcoTech.Domain.ServicesContract
{
    public interface IClient
    {       
       
        void Create(RegisterQuery query);        
        
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
        Task<LoginResponseDto> Authorize(int id);
             

        /// <summary>
        /// get user detail
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<UserDetailDto> GetUserDetailAsync(int userId, CancellationToken ct);


    }
}
