using Luna.Models.Tasks.Blank.CardAttributes;
using Luna.Models.Tasks.View.CardAttributes;

namespace Luna.Tasks.Services.Services.CardAttributes.Type;

public interface ITypeService
{
	public Task<IEnumerable<TypeView>> GetTypesAsync(Guid workspaceId);

	public Task<TypeView?> GetTypeAsync(Guid workspaceId, Guid typeId);

	public Task<TypeView?> GetTypeAsync(Guid typeId);

	public Task<Boolean> CreateTypeAsync(TypeBlank type, Guid userId);

	public Task<Boolean> UpdateTypeAsync(Guid id, TypeBlank type, Guid userId);

	public Task<Boolean> DeleteTypeAsync(Guid id);
}