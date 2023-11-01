using System.ComponentModel.DataAnnotations;

namespace MagicVilla;

public class CreateVillaDTO
{
	[Required]
	public string Name { get; set; } = null!;

	[Required]
	public string Details { get; set; } = null!;

	[Required]
	public double Rate { get; set; }

	[Required]
	public int Sqft { get; set; }

	[Required]
	public int Occupancy { get; set; }

	[Required]
	public string ImageUrl { get; set; } = null!;

	[Required]
	public string Amenity { get; set; } = null!;

	[Required]
	public DateTime CreatedDate { get; set; }

	[Required]
	public DateTime UpdatedDate { get; set; }
}
