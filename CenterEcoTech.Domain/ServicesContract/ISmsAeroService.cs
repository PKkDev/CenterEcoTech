using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenterEcoTech.Domain.ServicesContract
{
	public interface ISmsAeroService
	{
        /// <summary>
        /// Отправка SMS сообщения
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="code"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        void SmsSend(string phone, string code, CancellationToken ct);


    }
}
