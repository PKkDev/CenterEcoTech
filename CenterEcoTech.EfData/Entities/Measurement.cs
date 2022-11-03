using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CenterEcoTech.EfData.Entities
{
    public class Measurement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public double MeasurementValue { get; set; }

        public List<Client> Clients { get; set; }
        public List<ClientMeasurement> ClientMeasurements { get; set; }
    }
}
