using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagicVilla;

public class Villa
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public long Id { get; set; }
	[Required]
	public string Name { get; set; } = null!;
	[Required]
	public string Details { get; set; } = null!;
	[Required]
	public double Rate { get; set; }
	[Required]
	public int Sqft { get; set; }
	public int Occupancy { get; set; }
	[Required]
	public string ImageUrl { get; set; } = null!;
	[Required]
	public string Amenity { get; set; } = null!;
	public DateTime CreatedDate { get; set; }
	public DateTime UpdatedDate { get; set; }
}
