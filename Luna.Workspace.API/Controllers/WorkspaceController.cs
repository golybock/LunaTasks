using System.Security.Claims;
using Luna.Models.Workspace.Blank.Workspace;
using Luna.Models.Workspace.View.Workspace;
using Luna.Workspaces.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Luna.Workspace.API.Controllers;

// [Authorize]
[ApiController]
[Route("api/[controller]")]
public class WorkspaceController : ControllerBase
{
	private readonly IWorkspaceService _workspaceService;

	private Guid UserId => Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Authentication)!.Value);

	public WorkspaceController(IWorkspaceService workspaceService)
	{
		_workspaceService = workspaceService;
	}

	[HttpGet("[action]")]
	public async Task<IEnumerable<WorkspaceView>> GetWorkspacesAsync()
	{
		var workspaces = await _workspaceService.GetWorkspacesAsync();

		return workspaces;
	}

	[HttpGet("[action]")]
	public async Task<IActionResult> GetWorkspaceAsync(Guid id)
	{
		var workspace = await _workspaceService.GetWorkspaceAsync(id);

		if (workspace == null) return NotFound();

		return Ok(workspace);
	}

	[HttpGet("[action]")]
	public async Task<IEnumerable<WorkspaceView>> GetUserWorkspacesAsync()
	{
		var workspaces = await _workspaceService.GetWorkspacesByUserAsync(UserId);

		return workspaces;
	}

	[HttpGet("[action]")]
	public async Task<IEnumerable<WorkspaceView>> GetWorkspacesByCreatorAsync()
	{
		var workspaces = await _workspaceService.GetWorkspacesByCreatorAsync(UserId);

		return workspaces;
	}

	[HttpPost("[action]")]
	public async Task<IActionResult> CreateWorkspaceAsync(WorkspaceBlank workspaceBlank)
	{
		var res = await _workspaceService.CreateWorkspaceAsync(workspaceBlank, UserId);

		return res ? Ok() : BadRequest();
	}

	[HttpPut("[action]")]
	public async Task<IActionResult> UpdateWorkspaceAsync(Guid id, WorkspaceBlank workspaceBlank)
	{
		var res = await _workspaceService.UpdateWorkspaceAsync(id, workspaceBlank, UserId);

		return res ? Ok() : BadRequest();
	}

	[HttpDelete("[action]")]
	public async Task<IActionResult> DeleteWorkspaceAsync(Guid id, Guid userId)
	{
		var res = await _workspaceService.DeleteWorkspaceAsync(id, userId);

		return res ? Ok() : BadRequest();
	}

	[HttpPost("[action]")]
	public async Task<IActionResult> AddUserToWorkspace(Guid workspaceId, Guid userId)
	{
		var res = await _workspaceService.AddUserToWorkspace(workspaceId, userId, UserId);

		return res ? Ok() : BadRequest();
	}

	[HttpDelete("[action]")]
	public async Task<IActionResult> DeleteUserFromWorkspace(Guid workspaceId, Guid userId)
	{
		var res = await _workspaceService.DeleteUserFromWorkspace(workspaceId, userId, UserId);

		return res ? Ok() : BadRequest();
	}

	[HttpDelete("[action]")]
	public async Task<IActionResult> DeleteUsersFromWorkspace(Guid workspaceId)
	{
		var res = await _workspaceService.DeleteUsersFromWorkspace(workspaceId, UserId);

		return res ? Ok() : BadRequest();
	}

	[HttpDelete("[action]")]
	public async Task<IActionResult> DeleteUserFromWorkspaces(Guid userId)
	{
		var res = await _workspaceService.DeleteUserFromWorkspaces(userId);

		return res ? Ok() : BadRequest();
	}
}