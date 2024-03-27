using Luna.Models.Tasks.Blank.CardAttributes;
using Luna.Models.Tasks.Database.CardAttributes;
using Luna.Models.Tasks.Domain.CardAttributes;
using Luna.Models.Tasks.View.CardAttributes;
using Luna.Tasks.Repositories.Repositories.CardAttributes.Type;

namespace Luna.Tasks.Services.Services.CardAttributes.Type;

public class TypeService : ITypeService
{
	private readonly ITypeRepository _typeRepository;

	public TypeService(ITypeRepository typeRepository)
	{
		_typeRepository = typeRepository;
	}

	public async Task<IEnumerable<TypeView>> GetTypesAsync(Guid workspaceId)
	{
		var types = await _typeRepository.GetTypesAsync(workspaceId);

		return ToTypeViews(types);
	}

	public async Task<TypeView?> GetTypeAsync(Guid workspaceId, Guid typeId)
	{
		var type = await _typeRepository.GetTypeAsync(workspaceId, typeId);

		if (type == null)
			return null;

		return ToTypeView(type);
	}

	public async Task<TypeView?> GetTypeAsync(Guid typeId)
	{
		var type = await _typeRepository.GetTypeAsync(typeId);

		if (type == null)
			return null;

		return ToTypeView(type);
	}

	public async Task<IEnumerable<TypeDomain>> GetTypesDomainAsync(Guid workspaceId)
	{
		var types = await _typeRepository.GetTypesAsync(workspaceId);

		return ToTypeDomains(types);
	}

	public async Task<TypeDomain?> GetTypeDomainAsync(Guid workspaceId, Guid typeId)
	{
		var type = await _typeRepository.GetTypeAsync(workspaceId, typeId);

		if (type == null)
			return null;

		return ToTypeDomain(type);
	}

	public async Task<TypeDomain?> GetTypeDomainAsync(Guid typeId)
	{
		var type = await _typeRepository.GetTypeAsync(typeId);

		if (type == null)
			return null;

		return ToTypeDomain(type);
	}

	public async Task<bool> CreateTypeAsync(TypeBlank type, Guid userId)
	{
		var typeDatabase = ToTypeDatabase(type);

		var result = await _typeRepository.CreateTypeAsync(typeDatabase);

		return result;
	}

	public async Task<bool> UpdateTypeAsync(Guid id, TypeBlank type, Guid userId)
	{
		var typeDatabase = ToTypeDatabase(type);

		var result = await _typeRepository.UpdateTypeAsync(id, typeDatabase);

		return result;
	}

	public async Task<bool> DeleteTypeAsync(Guid id)
	{
		try
		{
			var result = await _typeRepository.DeleteTypeAsync(id);

			return result;
		}
		catch (Exception e)
		{
			return false;
		}
	}

	private TypeDatabase ToTypeDatabase(TypeBlank typeBlank)
	{
		return new TypeDatabase()
		{
			Id = Guid.NewGuid(),
			Name = typeBlank.Name,
			Deleted = false,
			HexColor = typeBlank.HexColor,
			WorkspaceId = typeBlank.WorkspaceId
		};
	}

	private TypeView ToTypeView(TypeDatabase typeDatabase)
	{
		var typeDomain = new TypeDomain(typeDatabase);
		return new TypeView(typeDomain);
	}

	private TypeDomain ToTypeDomain(TypeDatabase typeDatabase)
	{
		return new TypeDomain(typeDatabase);
	}

	private IEnumerable<TypeView> ToTypeViews(IEnumerable<TypeDatabase> typeDatabases)
	{
		return typeDatabases.Select(ToTypeView).ToList();
	}

	private IEnumerable<TypeDomain> ToTypeDomains(IEnumerable<TypeDatabase> typeDatabases)
	{
		return typeDatabases.Select(ToTypeDomain).ToList();
	}
}