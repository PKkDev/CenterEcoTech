using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CenterEcoTech.EfData.Entities
{
    public class СooperativeClient
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int СooperativeId { get; set; }
        public Сooperative Сooperative { get; set; }

        public int ClientId { get; set; }
        public Client Client { get; set; }
    }
}
