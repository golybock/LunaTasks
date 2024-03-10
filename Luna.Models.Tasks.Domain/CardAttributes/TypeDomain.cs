using Luna.Models.Tasks.Database.CardAttributes;

namespace Luna.Models.Tasks.Domain.CardAttributes;

public class TypeDomain
{
	public Guid Id { get; set; }

	public String Name { get; set; } = null!;

	private String HexColor { get; set; } = null!;

	public String Color => $"#{HexColor}";

	public Guid WorkspaceId { get; set; }

	public Boolean Deleted { get; set; }

	public TypeDomain(TypeDatabase typeDatabase)
	{
		Id = typeDatabase.Id;
		Name = typeDatabase.Name;
		HexColor = typeDatabase.HexColor;
		WorkspaceId = typeDatabase.WorkspaceId;
		Deleted = typeDatabase.Deleted;
	}
}