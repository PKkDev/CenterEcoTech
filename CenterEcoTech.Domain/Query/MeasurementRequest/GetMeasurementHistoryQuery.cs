using CenterEcoTech.EfData.Entities;

namespace CenterEcoTech.Domain.Query.MeasurementRequest
{
    public class GetMeasurementHistoryQuery
    {
        public DateTime? Date { get; set; }

        public string? Status { get; set; }
    }
}
