using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CenterEcoTech.EfData.Entities
{
    public class Request
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public RequestStatus Status { get; set; }

        public string Theme { get; set; }

        public string Message { get; set; }

        public int ClientId { get; set; }
        public Client Client { get; set; }
    }

    public enum RequestStatus
    {
        New = 1,
        Accepted = 2,
        InProgress = 3,
        Done = 4
    }
}
