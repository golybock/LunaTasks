using Luna.Models.Tasks.Database.CardAttributes;

namespace Luna.Models.Tasks.Domain.CardAttributes;

public class TagDomain
{
	public Guid Id { get; set; }

	public String Name { get; set; } = null!;

	private String HexColor { get; set; } = null!;

	public String Color => $"#{HexColor}";

	public Guid WorkspaceId { get; set; }

	public Boolean Deleted { get; set; }

	public TagDomain(TagDatabase tagDatabase)
	{
		Id = tagDatabase.Id;
		Name = tagDatabase.Name;
		HexColor = tagDatabase.HexColor;
		WorkspaceId = tagDatabase.WorkspaceId;
		Deleted = tagDatabase.Deleted;
	}
}