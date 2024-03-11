using Luna.Models.Tasks.Domain.CardAttributes;

namespace Luna.Models.Tasks.View.CardAttributes;

public class TagView
{
	public Guid Id { get; set; }

	public String Name { get; set; }

	public String Color { get; set; }

	public TagView(TagDomain tagDomain)
	{
		Id = tagDomain.Id;
		Name = tagDomain.Name;
		Color = tagDomain.Color;
	}
}