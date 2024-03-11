using Luna.Models.Tasks.Database.CardAttributes;
using Luna.Models.Tasks.Domain.CardAttributes;
using Luna.Models.Tasks.View.CardAttributes;
using Luna.Tasks.Repositories.Repositories.CardAttributes.Role;

namespace Luna.Tasks.Services.Services.CardAttributes.Role;

public class RoleService : IRoleService
{
	private readonly IRoleRepository _roleRepository;

	public RoleService(IRoleRepository roleRepository)
	{
		_roleRepository = roleRepository;
	}

	public async Task<IEnumerable<RoleView>> GetRolesAsync()
	{
		var roles = await _roleRepository.GetRolesAsync();

		return ToRoleViews(roles);
	}

	public async Task<RoleView?> GetRoleAsync(int roleId)
	{
		var role = await _roleRepository.GetRoleAsync(roleId);

		if (role == null)
			return null;

		return ToRoleView(role);
	}

	private RoleView ToRoleView(RoleDatabase roleDatabase)
	{
		var roleDomain = new RoleDomain(roleDatabase);
		return new RoleView(roleDomain);
	}

	private IEnumerable<RoleView> ToRoleViews(IEnumerable<RoleDatabase> roleDatabases)
	{
		return roleDatabases.Select(ToRoleView).ToList();
	}
}