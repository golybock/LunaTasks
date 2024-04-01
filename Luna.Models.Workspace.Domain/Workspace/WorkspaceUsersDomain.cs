using Luna.Models.Workspace.Database.Workspace;

namespace Luna.Models.Workspace.Domain.Workspace;

public class WorkspaceUsersDomain
{
	public Int32 Id { get; set; }

	public Guid UserId { get; set; }

	public Guid WorkspaceId { get; set; }

	public WorkspaceUsersDomain(WorkspaceUsersDatabase workspaceUsersDatabase)
	{
		Id = workspaceUsersDatabase.Id;
		UserId = workspaceUsersDatabase.UserId;
		WorkspaceId = workspaceUsersDatabase.WorkspaceId;
	}

	public WorkspaceUsersDomain()
	{
	}
}