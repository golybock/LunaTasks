using Luna.Models.Workspace.Blank.Workspace;

namespace Luna.Models.Workspace.Database.Workspace;

public class WorkspaceDatabase
{
	public Guid Id { get; set; }

	public String Name { get; set; } = null!;

	public DateTime CreatedTimestamp { get; set; }

	public Guid CreatedUserId { get; set; }

	public WorkspaceDatabase()
	{
	}

	public WorkspaceDatabase(Guid id, string name, DateTime createdTimestamp, Guid createdUserId)
	{
		Id = id;
		Name = name;
		CreatedTimestamp = createdTimestamp;
		CreatedUserId = createdUserId;
	}

	public WorkspaceDatabase(WorkspaceBlank workspaceBlank)
	{
		Name = workspaceBlank.Name;
	}
}