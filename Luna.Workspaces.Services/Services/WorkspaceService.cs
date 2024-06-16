using Luna.Models.Users.View.Users;
using Luna.Models.Workspace.Blank.Workspace;
using Luna.Models.Workspace.Database.Workspace;
using Luna.Models.Workspace.Domain.Workspace;
using Luna.Models.Workspace.View.Workspace;
using Luna.SharedDataAccess.Users.Services;
using Luna.Workspaces.Repositories.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

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

	public async Task<IActionResult> CreateWorkspaceAsync(WorkspaceBlank workspaceBlank, Guid userId)
	{
		var workspaceDatabase = new WorkspaceDatabase();

		workspaceDatabase.Id = Guid.NewGuid();
		workspaceDatabase.Name = workspaceBlank.Name;
		workspaceDatabase.CreatedTimestamp = DateTime.UtcNow;
		workspaceDatabase.CreatedUserId = userId;

		var res = await _workspaceRepository.CreateWorkspaceAsync(workspaceDatabase);

		return res ? new OkResult() : new BadRequestResult();
	}

	// todo add checks on owner userId
	public async Task<IActionResult> UpdateWorkspaceAsync(Guid id, WorkspaceBlank workspaceBlank, Guid userId)
	{
		var workspaceDatabase = new WorkspaceDatabase();

		workspaceDatabase.Name = workspaceBlank.Name;

		var res = await _workspaceRepository.UpdateWorkspaceAsync(id, workspaceDatabase);

		return res ? new OkResult() : new BadRequestResult();
	}

	// todo add checks on owner userId
	public async Task<IActionResult> DeleteWorkspaceAsync(Guid id, Guid operationBy)
	{
		var res = await _workspaceRepository.DeleteWorkspaceAsync(id);

		return res ? new OkResult() : new BadRequestResult();
	}

	// todo add checks on owner userId
	public async Task<IActionResult> AddUserToWorkspace(Guid workspaceId, Guid userId)
	{
		try
		{
			var res = await _workspaceRepository.AddUserToWorkspace(workspaceId, userId);

			return res ? new OkResult() : new BadRequestResult();
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			return new BadRequestResult();
		}
	}

	// todo add checks on owner userId
	public async Task<IActionResult> DeleteUserFromWorkspace(Guid workspaceId, Guid userId, Guid operationBy)
	{
		if (operationBy == userId)
		{
			return new BadRequestObjectResult("You can't delete yourself");
		}

		var workspace = await _workspaceRepository.GetWorkspaceAsync(workspaceId);

		if (workspace == null)
		{
			return new BadRequestObjectResult("Workspace not found");
		}

		if (workspace.CreatedUserId != operationBy)
		{
			return new BadRequestObjectResult("Only workspace admin can delete users");
		}

		var res = await _workspaceRepository.DeleteUserFromWorkspace(workspaceId, userId);

		return res ? new OkResult() : new BadRequestResult();
	}

	// todo add checks on owner userId
	public async Task<IActionResult> DeleteUsersFromWorkspace(Guid workspaceId, Guid operationBy)
	{
		var res = await _workspaceRepository.DeleteUsersFromWorkspace(workspaceId);

		return res ? new OkResult() : new BadRequestResult();
	}

	// todo add checks on owner userId
	public async Task<IActionResult> DeleteUserFromWorkspaces(Guid userId)
	{
		var res = await _workspaceRepository.DeleteUserFromWorkspaces(userId);

		return res ? new OkResult() : new BadRequestResult();
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