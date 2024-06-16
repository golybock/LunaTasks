using Luna.Models.Users.View.Users;
using Luna.Models.Workspace.Blank.Workspace;
using Luna.Models.Workspace.Domain.Workspace;
using Luna.Models.Workspace.View.Workspace;
using Microsoft.AspNetCore.Mvc;

namespace Luna.Workspaces.Services.Services;

public interface IWorkspaceService
{
	public Task<IEnumerable<WorkspaceView>> GetWorkspacesAsync();

	public Task<WorkspaceView?> GetWorkspaceAsync(Guid id);

	public Task<IEnumerable<WorkspaceView>> GetWorkspacesByUserAsync(Guid userId);

	public Task<IEnumerable<WorkspaceView>> GetWorkspacesByCreatorAsync(Guid userId);

	public Task<IEnumerable<Guid>> GetWorkspaceUserIdsAsync(Guid workspaceId);

	public Task<IEnumerable<UserView>> GetWorkspaceUsersAsync(Guid workspaceId);

	public Task<IActionResult> CreateWorkspaceAsync(WorkspaceBlank workspaceBlank, Guid userId);

	public Task<IActionResult> UpdateWorkspaceAsync(Guid id, WorkspaceBlank workspaceBlank, Guid userId);

	public Task<IActionResult> DeleteWorkspaceAsync(Guid id, Guid userId);

	public Task<IActionResult> AddUserToWorkspace(Guid workspaceId, Guid userId);

	public Task<IActionResult> DeleteUserFromWorkspace(Guid workspaceId, Guid userId, Guid operationBy);

	public Task<IActionResult> DeleteUsersFromWorkspace(Guid workspaceId, Guid operationBy);

	public Task<IActionResult> DeleteUserFromWorkspaces(Guid userId);
}