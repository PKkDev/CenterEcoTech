using CenterEcoTech.Domain.Exeptions;
using CenterEcoTech.Domain.ServicesContract;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CenterEcoTech.Infrastructure.Services
{
	public class SmsAeroService : ISmsAeroService
	{

        private readonly IHttpClientFactory _clientFactory;
        private readonly HttpClient _http = new HttpClient();


        /// <summary>
        /// Отправка SMS сообщения
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="code"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        /// <exception cref="ApiException"></exception>
        public async void SmsSend(string phone, string code, CancellationToken ct)
		{
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
        }
        	
	}
}
