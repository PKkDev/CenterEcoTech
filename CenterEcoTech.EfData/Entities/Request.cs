using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CenterEcoTech.EfData.Entities
{
    public class Request
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Aress { get; set; }

        public RequestType Type { get; set; }

        public DateTime DateSend { get; set; }

        public string Comment { get; set; }

        public int ClientId { get; set; }
        public Client Client { get; set; }
    }

    public enum RequestType
    {

    }
}
