using Microsoft.EntityFrameworkCore;

namespace MagicVilla;

public interface IVillaService
{
	public DbSet<Villa> FindAll();
	public Villa FindById(long id);
	public Villa Save(CreateVillaDTO createVillaDTO);
	public IEnumerable<VillaDTO> MapToDTO(DbSet<Villa> villas);
	public void Delete(long id);
	public void Update(long id, UpdateVillaDTO updateVillaDTO);

}
