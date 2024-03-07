using Luna.Models.Workspace.Database.Workspace;

namespace Luna.Users.Workspaces.Repositories.Repositories;

public interface IWorkspaceRepository
{
	public Task<IEnumerable<WorkspaceDatabase>> GetWorkspacesAsync();

	public Task<IEnumerable<WorkspaceDatabase>> GetWorkspacesByUserAsync(Guid userId);

	public Task<IEnumerable<WorkspaceDatabase>> GetWorkspacesByCreatorAsync(Guid userId);

	public Task<Boolean> CreateWorkspaceAsync(WorkspaceDatabase workspaceDatabase);

	public Task<Boolean> UpdateWorkspaceAsync(Guid id, WorkspaceDatabase workspaceDatabase);

	public Task<Boolean> DeleteWorkspaceAsync(Guid id);

	public Task<Boolean> AddUserToWorkspace(Guid workspaceId, Guid userId);

	public Task<Boolean> DeleteUserFromWorkspace(Guid workspaceId, Guid userId);

	public Task<Boolean> DeleteUsersFromWorkspace(Guid workspaceId);

	public Task<Boolean> DeleteUserFromWorkspaces(Guid userId);
}