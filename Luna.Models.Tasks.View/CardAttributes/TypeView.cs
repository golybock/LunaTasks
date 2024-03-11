using Luna.Models.Tasks.Domain.CardAttributes;

namespace Luna.Models.Tasks.View.CardAttributes;

public class TypeView
{
	public Guid Id { get; set; }

	public String Name { get; set; }

	public String Color { get; set; }

	public TypeView(TypeDomain typeDomain)
	{
		Id = typeDomain.Id;
		Name = typeDomain.Name;
		Color = typeDomain.Color;
	}
}