
using Microsoft.AspNetCore.JsonPatch;

namespace MagicVilla;

public static class VillaStore
{

	private static List<Villa> villas = new List<Villa>(){
			new Villa{Id=1,Name="Villa Depok"},
			new Villa{Id=2,Name="Villa Bogor"}
		};

	public static List<Villa> FindAll()
	{
		return villas;
	}

	public static Villa FindById(int id)
	{
		Villa? villa = villas.Find(villa => villa.Id == id);
		if (villa == null)
		{
			throw new ResourceNotFoundException($"Villa with id: {id} not found");
		}
		return villa;
	}

	public static Villa Save(CreateVillaRequest createVillaRequest)
	{
		Villa villa = new Villa { Id = NextId(), Name = createVillaRequest.Name };
		if (Exists(villa.Name))
		{
			throw new DuplicateResource($"Villa with name: {villa.Name} already exists");
		}
		villas.Add(villa);
		return villa;
	}

	public static void Delete(int id)
	{
		Villa? villa = villas.Find(villa => villa.Id == id);

		if (villa == null)
		{
			throw new ResourceNotFoundException($"Villa with id: {id} not found");
		}

		villas.Remove(villa);
	}

	public static void Update(int id, UpdateVillaRequest updateVillaRequest)
	{
		Villa villa = FindById(id);
		villa.Name = updateVillaRequest.Name;
	}
	// public static void Update(int id, JsonPatchDocument<UpdateVillaRequest> jsonPatchDocument)
	// {
	// 	Villa villa = FindById(id);
	// 	jsonPatchDocument.ApplyTo(villa);
	// 	// villa.Name = updateVillaRequest.Name;
	// }

	private static bool Exists(string name)
	{
		return villas.Exists(villa => villa.Name.ToLower() == name.ToLower());
	}
	private static int NextId()
	{
		return villas.Count + 1;
	}
}
