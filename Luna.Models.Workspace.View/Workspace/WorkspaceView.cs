using Luna.Models.Users.View.Users;
using Luna.Models.Workspace.Domain.Workspace;

namespace Luna.Models.Workspace.View.Workspace;

public class WorkspaceView
{
	public Guid Id { get; set; }

	public String Name { get; set; }

	public DateTime CreatedTimestamp { get; set; }

	public Guid CreatedUserId { get; set; }

	public IEnumerable<UserView> WorkspaceUsers { get; set; }

	public WorkspaceView(WorkspaceDomain workspaceDomain)
	{
		Id = workspaceDomain.Id;
		Name = workspaceDomain.Name;
		CreatedTimestamp = workspaceDomain.CreatedTimestamp;
		CreatedUserId = workspaceDomain.CreatedUserId;
		WorkspaceUsers = new List<UserView>();
	}

	public WorkspaceView(WorkspaceDomain workspaceDomain, IEnumerable<UserView> workspaceUsers)
	{
		Id = workspaceDomain.Id;
		Name = workspaceDomain.Name;
		CreatedTimestamp = workspaceDomain.CreatedTimestamp;
		CreatedUserId = workspaceDomain.CreatedUserId;
		WorkspaceUsers = workspaceUsers;
	}
}