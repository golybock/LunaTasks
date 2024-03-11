using Luna.Models.Tasks.View.CardAttributes;

namespace Luna.Tasks.Services.Services.CardAttributes.Role;

public interface IRoleService
{
	public Task<IEnumerable<RoleView>> GetRolesAsync();

	public Task<RoleView?> GetRoleAsync(Int32 roleId);
}