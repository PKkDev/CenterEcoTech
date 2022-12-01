namespace CenterEcoTech.Domain.ServicesContract
{
    public interface ISmsAeroService
    {
        /// <summary>
        /// send sms message
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="code"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        void SendAuthCode(string phone, string code, CancellationToken ct);
    }
}
