namespace Luna.Models.Workspace.Database.Workspace;

public class WorkspaceDatabase
{
	public Guid Id { get; set; }

	public String Name { get; set; } = null!;

	public DateTime CreatedTimestamp { get; set; }

	public Guid CreatedUserId { get; set; }
}