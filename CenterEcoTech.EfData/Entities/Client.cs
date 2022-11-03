using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CenterEcoTech.EfData.Entities
{
    public class Client
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastNme { get; set; }

        public string MidName { get; set; }

        public ClientAdress Adress { get; set; }

        public List<Сooperative> Сooperatives { get; set; }
        public List<СooperativeClient> СooperativeClientd { get; set; }

        public List<Measurement> Measurements { get; set; }
        public List<ClientMeasurement> ClientMeasurements { get; set; }
    }
}
