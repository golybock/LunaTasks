using Luna.Models.Users.View.Users;
using Luna.Models.Workspace.Blank.Workspace;
using Luna.Models.Workspace.Database.Workspace;
using Luna.Models.Workspace.Domain.Workspace;
using Luna.Models.Workspace.View.Workspace;
using Luna.SharedDataAccess.Users.Services;
using Luna.Workspaces.Repositories.Repositories;

namespace Luna.Workspaces.Services.Services;

public class WorkspaceService: IWorkspaceService
{
	private readonly IWorkspaceRepository _workspaceRepository;
	private readonly IUserService _userService;

	public WorkspaceService(IWorkspaceRepository workspaceRepository, IUserService userService)
	{
		_workspaceRepository = workspaceRepository;
		_userService = userService;
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

		var workspaceUsers = await GetWorkspaceUserIdsAsync(id);

		var users = await _userService.GetUsersAsync();

		if (workspace == null) return null;

		return WorkspaceToView(workspace, users.Where(user => workspaceUsers.Contains(user.Id)).ToList());
	}

	public async Task<IEnumerable<WorkspaceView>> GetWorkspacesByUserAsync(Guid userId)
	{
		var joinedWorkspaces = await _workspaceRepository.GetWorkspacesByUserAsync(userId);

		var createdWorkspaces = await _workspaceRepository.GetWorkspacesByCreatorAsync(userId);

		var workspaces = joinedWorkspaces.ToList();
		workspaces.AddRange(createdWorkspaces);

		var workspacesView = WorkspacesToView(workspaces);

		return workspacesView;
	}

	public async Task<IEnumerable<WorkspaceView>> GetWorkspacesByCreatorAsync(Guid userId)
	{
		var workspaces = await _workspaceRepository.GetWorkspacesByCreatorAsync(userId);

		var workspacesView = WorkspacesToView(workspaces);

		return workspacesView;
	}

	public async Task<IEnumerable<Guid>> GetWorkspaceUserIdsAsync(Guid workspaceId)
	{
		var workspaceUsers  = await _workspaceRepository.GetWorkspaceUsersAsync(workspaceId);

		var workspaceUsersIds = workspaceUsers.Select(user => user.UserId);

		return workspaceUsersIds;
	}

	public async Task<IEnumerable<UserView>> GetWorkspaceUsersAsync(Guid workspaceId)
	{
		var workspace = await GetWorkspaceAsync(workspaceId);

		if (workspace == null)
		{
			return Array.Empty<UserView>();
		}

		var workspaceUsers = await GetWorkspaceUserIdsAsync(workspaceId);

		var users = await _userService.GetUsersAsync();

		var workspaceUsersView = users.Where(user => workspaceUsers.Contains(user.Id) || user.Id == workspace.CreatedUserId).ToList();

		return workspaceUsersView;
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
	public async Task<Boolean> AddUserToWorkspace(Guid workspaceId, Guid userId)
	{
		try
		{
			return await _workspaceRepository.AddUserToWorkspace(workspaceId, userId);
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			return false;
		}
	}

	// todo add checks on owner userId
	public async Task<Boolean> DeleteUserFromWorkspace(Guid workspaceId, Guid userId, Guid operationBy)
	{
		if (operationBy == userId)
		{
			return false;
		}

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

	private WorkspaceView WorkspaceToView(WorkspaceDatabase workspaceDatabases, IEnumerable<UserView> userViews)
	{
		var workspacesDomain = new WorkspaceDomain(workspaceDatabases);

		return new WorkspaceView(workspacesDomain, userViews);
	}
}