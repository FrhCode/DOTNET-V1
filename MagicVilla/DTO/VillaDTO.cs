﻿namespace MagicVilla;

public class VillaDTO
{
	public long Id { get; set; }
	public string Name { get; set; } = null!;
	public string Details { get; set; } = null!;
	public double Rate { get; set; }
	public int Sqft { get; set; }
	public int Occupancy { get; set; }
	public string ImageUrl { get; set; } = null!;
	public string Amenity { get; set; } = null!;
	public DateTime CreatedDate { get; set; }
	public DateTime UpdatedDate { get; set; }
}
