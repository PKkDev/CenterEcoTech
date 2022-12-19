using CenterEcoTech.EfData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenterEcoTech.Domain.DTO.MeasurementRequest
{
	public class MeasurementRequestDto
	{
		public DateTime Date { get; set; }

		public string Name { get; set; }

		public double Value { get; set; }

		public string CLientName { get; set; }

		public string CLientAdress { get; set; }
	}
}
