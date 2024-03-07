using Luna.Models.Workspace.Blank.Workspace;
using Luna.Models.Workspace.Database.Workspace;
using Luna.Models.Workspace.Domain.Workspace;
using Luna.Models.Workspace.View.Workspace;
using Luna.Workspaces.Repositories.Repositories;

namespace Luna.Workspaces.Services.Services;

public class WorkspaceService: IWorkspaceService
{
	private readonly IWorkspaceRepository _workspaceRepository;

	public WorkspaceService(IWorkspaceRepository workspaceRepository)
	{
		_workspaceRepository = workspaceRepository;
	}

	public async Task<IEnumerable<WorkspaceView>> GetWorkspacesAsync()
	{
		var workspaces = await _workspaceRepository.GetWorkspacesAsync();

		var workspacesView = WorkspacesToView(workspaces);

		return workspacesView;
	}

	// todo add checks on owner userId
	public async Task<WorkspaceView?> GetWorkspaceAsync(Guid id)
	{
		var workspace = await _workspaceRepository.GetWorkspaceAsync(id);

		if (workspace == null) return null;

		return WorkspaceToView(workspace);
	}

	public async Task<IEnumerable<WorkspaceView>> GetWorkspacesByUserAsync(Guid userId)
	{
		var workspaces = await _workspaceRepository.GetWorkspacesByUserAsync(userId);

		var workspacesView = WorkspacesToView(workspaces);

		return workspacesView;
	}

	public async Task<IEnumerable<WorkspaceView>> GetWorkspacesByCreatorAsync(Guid userId)
	{
		var workspaces = await _workspaceRepository.GetWorkspacesByCreatorAsync(userId);

		var workspacesView = WorkspacesToView(workspaces);

		return workspacesView;
	}

	public async Task<Boolean> CreateWorkspaceAsync(WorkspaceBlank workspaceBlank, Guid userId)
	{
		var workspaceDatabase = new WorkspaceDatabase();

		workspaceDatabase.Id = Guid.NewGuid();
		workspaceDatabase.Name = workspaceBlank.Name;
		workspaceDatabase.CreatedTimestamp = DateTime.UtcNow;
		workspaceDatabase.CreatedUserId = userId;

		return await _workspaceRepository.CreateWorkspaceAsync(workspaceDatabase);
	}

	// todo add checks on owner userId
	public async Task<Boolean> UpdateWorkspaceAsync(Guid id, WorkspaceBlank workspaceBlank, Guid userId)
	{
		var workspaceDatabase = new WorkspaceDatabase();

		workspaceDatabase.Name = workspaceBlank.Name;

		return await _workspaceRepository.UpdateWorkspaceAsync(id, workspaceDatabase);
	}

	// todo add checks on owner userId
	public async Task<Boolean> DeleteWorkspaceAsync(Guid id, Guid operationBy)
	{
		return await _workspaceRepository.DeleteWorkspaceAsync(id);
	}

	// todo add checks on owner userId
	public async Task<Boolean> AddUserToWorkspace(Guid workspaceId, Guid userId, Guid operationBy)
	{
		return await _workspaceRepository.AddUserToWorkspace(workspaceId, userId);
	}

	// todo add checks on owner userId
	public async Task<Boolean> DeleteUserFromWorkspace(Guid workspaceId, Guid userId, Guid operationBy)
	{
		return await _workspaceRepository.DeleteUserFromWorkspace(workspaceId, userId);
	}

	// todo add checks on owner userId
	public async Task<Boolean> DeleteUsersFromWorkspace(Guid workspaceId, Guid operationBy)
	{
		return await _workspaceRepository.DeleteUsersFromWorkspace(workspaceId);
	}

	// todo add checks on owner userId
	public async Task<Boolean> DeleteUserFromWorkspaces(Guid userId)
	{
		return await _workspaceRepository.DeleteUserFromWorkspaces(userId);
	}

	private IEnumerable<WorkspaceView> WorkspacesToView(IEnumerable<WorkspaceDatabase> workspaceDatabases)
	{
		var workspacesDomain = workspaceDatabases.Select(w => new WorkspaceDomain(w));

		return workspacesDomain.Select(v => new WorkspaceView(v));
	}

	private WorkspaceView WorkspaceToView(WorkspaceDatabase workspaceDatabases)
	{
		var workspacesDomain = new WorkspaceDomain(workspaceDatabases);

		return new WorkspaceView(workspacesDomain);
	}
}