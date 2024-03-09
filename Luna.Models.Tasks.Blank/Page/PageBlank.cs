namespace Luna.Models.Tasks.Blank.Page;

public class PageBlank
{
	public String Name { get; set; } = null!;

	public String Description { get; set; } = null!;

	public String HeaderImage { get; set; } = null!;

	public Guid WorkspaceId { get; set; }
}