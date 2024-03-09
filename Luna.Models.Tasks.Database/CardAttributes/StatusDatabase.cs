namespace Luna.Models.Tasks.Database.CardAttributes;

public class StatusDatabase
{
	public Guid Id { get; set; }

	public String Name { get; set; } = null!;

	public String HexColor { get; set; } = null!;

	public Guid WorkspaceId { get; set; }

	public Boolean Deleted { get; set; }
}