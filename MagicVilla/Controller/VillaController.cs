using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla;

[Route("api/v1/villas")]
[ApiController]
public class VillaController : ControllerBase
{
	private readonly ILogger<VillaController> _logger;

	public VillaController(ILogger<VillaController> logger)
	{
		_logger = logger;
	}

	[HttpGet]
	public ActionResult<IEnumerable<VillaDTO>> FindAll()
	{
		IEnumerable<VillaDTO> villaDTOs = VillaStore.FindAll().Select(villa => new VillaDTO { Id = villa.Id, Name = villa.Name });
		return Ok(villaDTOs);
	}

	[HttpGet("{id}", Name = "FindById")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public ActionResult<VillaDTO> FindById(int id)
	{
		try
		{
			Villa villa = VillaStore.FindById(id);
			return Ok(new VillaDTO { Id = villa.Id, Name = villa.Name });
		}
		catch (ResourceNotFoundException Ex)
		{
			_logger.LogWarning(Ex.Message);
			return NotFound();
		}

	}

	[HttpPost]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status201Created)]
	public ActionResult<VillaDTO> Save([FromBody] CreateVillaRequest createVillaRequest)
	{

		try
		{
			Villa villa = VillaStore.Save(createVillaRequest);
			return CreatedAtRoute("FindById", new { id = villa.Id }, new VillaDTO { Id = villa.Id, Name = villa.Name });
		}
		catch (DuplicateResource Ex)
		{
			_logger.LogWarning(Ex.Message);
			ModelState.AddModelError("DuplicateResource", Ex.Message);
			return BadRequest(ModelState);
		}
	}

	[HttpDelete("{id}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public ActionResult Delete(int id)
	{
		try
		{
			VillaStore.Delete(id);
			return NoContent();
		}
		catch (ResourceNotFoundException Ex)
		{
			_logger.LogWarning(Ex.Message);
			return NotFound();
		}
	}

	[HttpPut("{id}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public ActionResult Update(int id, [FromBody] UpdateVillaRequest updateVillaRequest)
	{
		try
		{
			VillaStore.Update(id, updateVillaRequest);
			return NoContent();
		}
		catch (ResourceNotFoundException Ex)
		{

			_logger.LogWarning(Ex.Message);
			return NotFound();
		}
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
