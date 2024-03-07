namespace Luna.Models.Workspace.Database.Workspace;

public class WorkspaceUsersDatabase
{
	public Int32 Id { get; set; }

	public Guid UserId { get; set; }

	public Guid WorkspaceId { get; set; }
}