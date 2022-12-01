using CenterEcoTech.Domain.Exeptions;
using CenterEcoTech.Domain.ServicesContract;
using Microsoft.AspNetCore.WebUtilities;

namespace CenterEcoTech.Infrastructure.Services
{
    public class SmsAeroService : ISmsAeroService
    {
        private readonly IHttpClientFactory _clientFactory;

        public SmsAeroService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        /// <summary>
        /// send sms message
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="code"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        /// <exception cref="ApiException"></exception>
        public async void SendAuthCode(string phone, string code, CancellationToken ct)
        {
            var client = _clientFactory.CreateClient("smsAreaApi");

            if (client == null) throw new Exception("http client not creted");

            Dictionary<string, string> queryParam = new()
            {
                {"number", $"{phone}"},
                {"text", $"код для доступа: {code}"},
                {"sign", "SMS Aero"}
            };
            var uri = QueryHelpers.AddQueryString(client.BaseAddress?.AbsoluteUri, queryParam);
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var response = await client.SendAsync(request, ct);

            if (!response.IsSuccessStatusCode)
            {
                var responseMessage = await response.Content.ReadAsStringAsync(ct);
                throw new ApiException("error on send sms");
            }
        }

    }
}
