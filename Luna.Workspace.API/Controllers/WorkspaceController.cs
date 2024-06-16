using System.Security.Claims;
using Luna.Models.Users.View.Users;
using Luna.Models.Workspace.Blank.Workspace;
using Luna.Models.Workspace.View.Workspace;
using Luna.Workspaces.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ControllerBase = Luna.Tools.Web.ControllerBase;

namespace Luna.Workspace.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class WorkspaceController : ControllerBase
{
	private readonly IWorkspaceService _workspaceService;

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
		return await _workspaceService.CreateWorkspaceAsync(workspaceBlank, UserId);
	}

	[HttpPut("[action]")]
	public async Task<IActionResult> UpdateWorkspaceAsync(Guid id, WorkspaceBlank workspaceBlank)
	{
		return await _workspaceService.UpdateWorkspaceAsync(id, workspaceBlank, UserId);
	}

	[HttpDelete("[action]")]
	public async Task<IActionResult> DeleteWorkspaceAsync(Guid id)
	{
		return await _workspaceService.DeleteWorkspaceAsync(id, UserId);
	}

	[HttpGet("[action]")]
	public async Task<IEnumerable<UserView>> GetWorkspaceUsersAsync(Guid workspaceId)
	{
		return await _workspaceService.GetWorkspaceUsersAsync(workspaceId);
	}

	[HttpPost("[action]")]
	public async Task<IActionResult> JoinToWorkspace(Guid workspaceId)
	{
		return await _workspaceService.AddUserToWorkspace(workspaceId, UserId);
	}

	[HttpDelete("[action]")]
	public async Task<IActionResult> DeleteUserFromWorkspace(Guid workspaceId, Guid userId)
	{
		return await _workspaceService.DeleteUserFromWorkspace(workspaceId, userId, UserId);
	}

	[HttpDelete("[action]")]
	public async Task<IActionResult> DeleteUsersFromWorkspace(Guid workspaceId)
	{
		return await _workspaceService.DeleteUsersFromWorkspace(workspaceId, UserId);
	}

	[HttpDelete("[action]")]
	public async Task<IActionResult> DeleteUserFromWorkspaces(Guid userId)
	{
		return await _workspaceService.DeleteUserFromWorkspaces(userId);
	}
}