using CenterEcoTech.EfData.Entities;

namespace CenterEcoTech.Domain.Query.ClientRequest
{
    public class GetClientRequestHistoryQuery
    {
        public DateTime? Date { get; set; }

        public RequestStatus? Status { get; set; }
    }
}
