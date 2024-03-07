using Luna.Models.Workspace.Blank.Workspace;
using Luna.Models.Workspace.View.Workspace;

namespace Luna.Workspaces.Services.Services;

public interface IWorkspaceService
{
	public Task<IEnumerable<WorkspaceView>> GetWorkspacesAsync();

	public Task<WorkspaceView?> GetWorkspaceAsync(Guid id);

	public Task<IEnumerable<WorkspaceView>> GetWorkspacesByUserAsync(Guid userId);

	public Task<IEnumerable<WorkspaceView>> GetWorkspacesByCreatorAsync(Guid userId);

	public Task<Boolean> CreateWorkspaceAsync(WorkspaceBlank workspaceBlank, Guid userId);

	public Task<Boolean> UpdateWorkspaceAsync(Guid id, WorkspaceBlank workspaceBlank, Guid userId);

	public Task<Boolean> DeleteWorkspaceAsync(Guid id, Guid userId);

	public Task<Boolean> AddUserToWorkspace(Guid workspaceId, Guid userId, Guid operationBy);

	public Task<Boolean> DeleteUserFromWorkspace(Guid workspaceId, Guid userId, Guid operationBy);

	public Task<Boolean> DeleteUsersFromWorkspace(Guid workspaceId, Guid operationBy);

	public Task<Boolean> DeleteUserFromWorkspaces(Guid userId);
}