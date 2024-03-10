using Luna.Models.Tasks.Database.CardAttributes;

namespace Luna.Models.Tasks.Domain.CardAttributes;

public class RoleDomain
{
	public Int32 Id { get; set; }

	public String Name { get; set; } = null!;

	public RoleDomain(RoleDatabase roleDatabase)
	{
		Id = roleDatabase.Id;
		Name = roleDatabase.Name;
	}
}