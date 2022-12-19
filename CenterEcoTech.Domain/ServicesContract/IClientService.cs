using CenterEcoTech.Domain.DTO;
using CenterEcoTech.Domain.DTO.User;
using CenterEcoTech.Domain.Query;

namespace CenterEcoTech.Domain.ServicesContract
{
    public interface IClientService
    {
        /// <summary>
        /// register user
        /// </summary>
        /// <param name="query"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task RegisterUserAsync(RegisterQuery query, CancellationToken ct);

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
        /// get user client
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<UserDetailDto> GetClientDetailAsync(int userId, CancellationToken ct);

        /// <summary>
        /// update client detail
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="detail"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task UpdateClientDetailAsync(int userId, UserDetailDto detail, CancellationToken ct);

        /// <summary>
        /// delete client
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task DeleteClientAsync(int userId, CancellationToken ct);

    }
}
