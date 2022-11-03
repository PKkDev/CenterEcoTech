using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CenterEcoTech.EfData.Entities
{
    public class Сooperative
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Adress { get; set; }

        public string Phone { get; set; }

        public List<Client> Clients { get; set; }
        public List<СooperativeClient> СooperativeClients { get; set; }
    }
}
