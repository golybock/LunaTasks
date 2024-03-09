namespace Luna.Models.Tasks.Domain.CardAttributes;

public class StatusDomain
{
	public Guid Id { get; set; }

	public String Name { get; set; } = null!;

	private String HexColor { get; set; } = null!;

	public String Color => $"#{HexColor}";

	public Guid WorkspaceId { get; set; }

	public Boolean Deleted { get; set; }
}