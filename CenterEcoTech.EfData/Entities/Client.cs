using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace CenterEcoTech.EfData.Entities
{
    [Index(nameof(Phone), IsUnique = true)]
    [Index(nameof(Email), IsUnique = true)]
    public class Client
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Phone { get; set; }

        public string? Email { get; set; }

        public string FirstName { get; set; }

        public string? LastNme { get; set; }

        public string? MidName { get; set; }

        public ClientAdress? Adress { get; set; }

        public int CooperativeId { get; set; }
        public Cooperative Cooperative { get; set; }

        public List<Request> Requests { get; set; }

        public List<Counter> Counters { get; set; }

        public Client()
        {
            Requests = new();
            Counters = new();
        }

        public string GetFullName()
        {
            return $"{LastNme} {FirstName} {MidName}";
        }

        public string GetFullAdress()
        {
            if (Adress == null) return String.Empty;
            return $"{Adress.City} {Adress.Street} {Adress.House}";
        }
    }
}
