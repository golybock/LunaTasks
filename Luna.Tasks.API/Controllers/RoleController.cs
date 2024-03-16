using Luna.Models.Tasks.View.CardAttributes;
using Luna.Tasks.Services.Services.CardAttributes.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Luna.Tasks.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class RoleController: ControllerBase
{
	private readonly IRoleService _roleService;

	public RoleController(IRoleService roleService)
	{
		_roleService = roleService;
	}

	[HttpGet("[action]")]
	public async Task<IEnumerable<RoleView>> GetRolesAsync()
	{
		return await _roleService.GetRolesAsync();
	}

	[HttpGet("[action]")]
	public async Task<RoleView?> GetRoleAsync(int roleId)
	{
		return await _roleService.GetRoleAsync(roleId);
	}
}