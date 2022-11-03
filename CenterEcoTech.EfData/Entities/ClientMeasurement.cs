using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CenterEcoTech.EfData.Entities
{
    public class ClientMeasurement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int MeasurementId { get; set; }
        public Measurement Measurement { get; set; }

        public int ClientId { get; set; }
        public Client Client { get; set; }
    }
}
