using Luna.Models.Tasks.Domain.CardAttributes;

namespace Luna.Models.Tasks.View.CardAttributes;

public class StatusView
{
	public Guid Id { get; set; }

	public String Name { get; set; }

	public String Color { get; set; }

	public StatusView(StatusDomain statusDomain)
	{
		Id = statusDomain.Id;
		Name = statusDomain.Name;
		Color = statusDomain.Color;
	}
}