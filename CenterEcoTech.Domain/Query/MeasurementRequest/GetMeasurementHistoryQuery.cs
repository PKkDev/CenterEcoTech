using CenterEcoTech.EfData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenterEcoTech.Domain.Query.MeasurementRequest
{
	public class GetMeasurementHistoryQuery
	{
		public DateTime? Date { get; set; }

		public RequestStatus? Status { get; set; }
	}
}
