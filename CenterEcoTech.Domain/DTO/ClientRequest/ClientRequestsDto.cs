using CenterEcoTech.EfData.Entities;

namespace CenterEcoTech.Domain.DTO.ClientRequest
{
    public class ClientRequestsDto
    {
        public DateTime Date { get; set; }

        public string Theme { get; set; }

        public string Message { get; set; }

        public string CLientName { get; set; }

        public string CLientAdress { get; set; }

        public RequestStatus Status { get; set; }
    }
}
