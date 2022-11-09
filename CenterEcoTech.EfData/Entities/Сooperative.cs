using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace CenterEcoTech.EfData.Entities
{
    [Index(nameof(Name), IsUnique = true)]
    [Index(nameof(Phone), IsUnique = true)]
    public class Сooperative
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Adress { get; set; }

        public string Phone { get; set; }

        public List<Client> Clients { get; set; }

        public Сooperative()
        {
            Clients = new();
        }
    }
}
