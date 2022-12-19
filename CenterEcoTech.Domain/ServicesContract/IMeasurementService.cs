using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CenterEcoTech.Domain.DTO;
using CenterEcoTech.Domain.DTO.MeasurementRequest;
using CenterEcoTech.Domain.DTO.User;
using CenterEcoTech.Domain.Query;

namespace CenterEcoTech.Domain.ServicesContract
{
	public interface IMeasurementService
	{

		/// <summary>
		/// get mesuarement history
		/// </summary>
		/// <param name="userId"></param>
		/// <param name="ct"></param>
		/// <returns></returns>
		Task<MeasurementRequestDto> GetHistoryMeasurement(int userId, CancellationToken ct);
	}
}
