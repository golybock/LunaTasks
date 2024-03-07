using Luna.Models.Workspace.Database.Workspace;

namespace Luna.Models.Workspace.Domain.Workspace;

public class WorkspaceDomain
{
	public Guid Id { get; set; }

	public String Name { get; set; }

	public DateTime CreatedTimestamp { get; set; }

	public Guid CreatedUserId { get; set; }

	public IEnumerable<Guid> WorkspaceUsersDomains { get; set; }

	public WorkspaceDomain(WorkspaceDatabase workspaceDatabase)
	{
		Id = workspaceDatabase.Id;
		Name = workspaceDatabase.Name;
		CreatedTimestamp = workspaceDatabase.CreatedTimestamp;
		CreatedUserId = workspaceDatabase.CreatedUserId;
		WorkspaceUsersDomains = new List<Guid>();
	}

	public WorkspaceDomain(WorkspaceDatabase workspaceDatabase, IEnumerable<Guid> workspaceUsersDomains)
	{
		Id = workspaceDatabase.Id;
		Name = workspaceDatabase.Name;
		CreatedTimestamp = workspaceDatabase.CreatedTimestamp;
		CreatedUserId = workspaceDatabase.CreatedUserId;
		WorkspaceUsersDomains = workspaceUsersDomains;
	}
}