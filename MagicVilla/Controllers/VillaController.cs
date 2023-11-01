using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla;

[Route("api/v1/villas")]
[ApiController]
public class VillaController : ControllerBase
{

	private readonly IVillaService _villaService;
	public VillaController(IVillaService villaService)
	{
		_villaService = villaService;
	}



	[HttpGet]
	public ActionResult<DbSet<Villa>> FindAll()
	{
		return Ok(_villaService.MapToDTO(_villaService.FindAll()));
	}

	[HttpGet("{id}", Name = "FindById")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public ActionResult<VillaDTO> FindById(int id)
	{
		return Ok(_villaService.FindById(id));
	}

	[HttpPost]
	[ProducesResponseType(StatusCodes.Status201Created)]
	public ActionResult<VillaDTO> Save([FromBody] CreateVillaDTO createVillaDTO)
	{

		Villa villa = _villaService.Save(createVillaDTO);

		return CreatedAtRoute("FindById", new { id = villa.Id }, new VillaDTO { Id = villa.Id, Amenity = villa.Amenity, CreatedDate = villa.CreatedDate, Details = villa.Details, ImageUrl = villa.ImageUrl, Name = villa.Name, Occupancy = villa.Occupancy, Rate = villa.Rate, Sqft = villa.Sqft, UpdatedDate = villa.UpdatedDate });

	}

	[HttpDelete("{id}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public ActionResult Delete(int id)
	{
		_villaService.Delete(id);
		return NoContent();
	}

	[HttpPut("{id}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public ActionResult Update(int id, [FromBody] UpdateVillaDTO updateVillaDTO)
	{
		_villaService.Update(id, updateVillaDTO);
		return NoContent();
	}

	// [HttpPatch("{id}")]
	// [ProducesResponseType(StatusCodes.Status204NoContent)]
	// [ProducesResponseType(StatusCodes.Status404NotFound)]
	// public ActionResult Update(int id, JsonPatchDocument<UpdateVillaRequest> jsonPatchDocument)
	// {
	// 	try
	// 	{
	// 		return NoContent();
	// 	}
	// 	catch (ResourceNotFoundException ex)
	// 	{
	// 		Console.WriteLine(ex);
	// 		return NotFound();
	// 	}

	// }
}
