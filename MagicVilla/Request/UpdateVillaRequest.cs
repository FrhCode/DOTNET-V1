using System.ComponentModel.DataAnnotations;

namespace MagicVilla;

public class UpdateVillaRequest
{
	[Required(ErrorMessage = "Name is required")]
	[MinLength(3, ErrorMessage = "Name must be at least 3 characters long")]
	public string Name { get; set; }
}
