namespace Luna.Models.Tasks.Database.Page;

public class PageDatabase
{
	public Guid Id { get; set; }

	public String Name { get; set; } = null!;

	public String Description { get; set; } = null!;

	public String HeaderImage { get; set; } = null!;

	public DateTime CreatedTimestamp { get; set; }

	public Guid CreatedUserId { get; set; }

	public Guid WorkspaceId { get; set; }

	public Boolean Deleted { get; set; }
}