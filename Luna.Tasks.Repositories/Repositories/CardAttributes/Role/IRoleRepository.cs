using Luna.Models.Tasks.Database.CardAttributes;

namespace Luna.Tasks.Repositories.Repositories.CardAttributes.Role;

public interface IRoleRepository
{
	public Task<IEnumerable<RoleDatabase>> GetRolesAsync();

	public Task<RoleDatabase?> GetRoleAsync(Int32 roleId);

	public Task<Boolean> CreateRoleAsync(RoleDatabase role);
}