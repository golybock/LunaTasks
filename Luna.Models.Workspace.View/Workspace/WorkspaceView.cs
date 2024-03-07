using Luna.Models.Workspace.Domain.Workspace;

namespace Luna.Models.Workspace.View.Workspace;

public class WorkspaceView
{
	public Guid Id { get; set; }

	public String Name { get; set; }

	public DateTime CreatedTimestamp { get; set; }

	public Guid CreatedUserId { get; set; }

	public IEnumerable<Guid> WorkspaceUsersDomains { get; set; }

	public WorkspaceView(WorkspaceDomain workspaceDomain)
	{
		Id = workspaceDomain.Id;
		Name = workspaceDomain.Name;
		CreatedTimestamp = workspaceDomain.CreatedTimestamp;
		CreatedUserId = workspaceDomain.CreatedUserId;
		WorkspaceUsersDomains = new List<Guid>();
	}

	public WorkspaceView(WorkspaceDomain workspaceDomain, IEnumerable<Guid> workspaceUsers)
	{
		Id = workspaceDomain.Id;
		Name = workspaceDomain.Name;
		CreatedTimestamp = workspaceDomain.CreatedTimestamp;
		CreatedUserId = workspaceDomain.CreatedUserId;
		WorkspaceUsersDomains = workspaceUsers;
	}
}