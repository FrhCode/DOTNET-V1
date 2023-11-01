using Microsoft.EntityFrameworkCore;

namespace MagicVilla;

public class VillaService : IVillaService
{
	private readonly ApplicationDbContext _applicationDbContext;
	public VillaService(ApplicationDbContext applicationDbContext)
	{
		_applicationDbContext = applicationDbContext;
	}

	public void Delete(long id)
	{
		_applicationDbContext.Villas.Remove(FindById(id));
		_applicationDbContext.SaveChanges();
	}

	public DbSet<Villa> FindAll()
	{
		return _applicationDbContext.Villas;
	}

	public Villa FindById(long id)
	{
		Villa? villa = _applicationDbContext.Villas.Find(id);
		if (villa == null)
		{
			throw new ResourceNotFoundException($"Villa with id {id} not found");
		}
		return villa;
	}

	public IEnumerable<VillaDTO> MapToDTO(DbSet<Villa> villas)
	{
		IQueryable<VillaDTO> villaDTOs = villas.Select(v => new VillaDTO { Id = v.Id, Amenity = v.Amenity, CreatedDate = v.CreatedDate, Details = v.Details, ImageUrl = v.ImageUrl, Name = v.Name, Occupancy = v.Occupancy, Rate = v.Rate, Sqft = v.Sqft, UpdatedDate = v.UpdatedDate });
		return villaDTOs;
	}

	public Villa Save(CreateVillaDTO createVillaDTO)
	{
		Villa villa = new Villa() { Amenity = createVillaDTO.Amenity, CreatedDate = createVillaDTO.CreatedDate, Details = createVillaDTO.Details, ImageUrl = createVillaDTO.ImageUrl, Name = createVillaDTO.Name, Occupancy = createVillaDTO.Occupancy, Rate = createVillaDTO.Rate, Sqft = createVillaDTO.Sqft, UpdatedDate = createVillaDTO.UpdatedDate };
		_applicationDbContext.Villas.Add(villa);
		_applicationDbContext.SaveChanges();

		return _applicationDbContext.Villas.FirstOrDefault(v => v.Name == villa.Name)!;

	}

	public void Update(long id, UpdateVillaDTO updateVillaDTO)
	{
		Villa villa = FindById(id);
		villa.Name = updateVillaDTO.Name;
		villa.Details = updateVillaDTO.Details;
		villa.Rate = updateVillaDTO.Rate;
		villa.Sqft = updateVillaDTO.Sqft;
		villa.Occupancy = updateVillaDTO.Occupancy;
		villa.ImageUrl = updateVillaDTO.ImageUrl;
		villa.Amenity = updateVillaDTO.Amenity;

		_applicationDbContext.SaveChanges();
	}
}
