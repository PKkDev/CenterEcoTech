using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace CenterEcoTech.EfData.Entities
{
	[Index(nameof(Name), IsUnique = true)]
	public class Counter
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string Name { get; set; }

		public string? Postfix { get; set; }
		public List<Measurement> Measurements { get; set; }
		public int ClientId { get; set; }
		public Client Client { get; set; }
		public Counter()
		{
			Measurements = new();
		}

	}
}
