using Luna.Models.Tasks.Domain.CardAttributes;

namespace Luna.Models.Tasks.View.CardAttributes;

public class RoleView
{
	public Int32 Id { get; set; }

	public String Name { get; set; }

	public RoleView(RoleDomain roleDomain)
	{
		Id = roleDomain.Id;
		Name = roleDomain.Name;
	}
}