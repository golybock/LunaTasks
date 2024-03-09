using Luna.Models.Tasks.View.Card;

namespace Luna.Models.Tasks.View.Page;

public class PageView
{
	public Guid Id { get; set; }

	public String Name { get; set; } = null!;

	public String Description { get; set; } = null!;

	public String HeaderImage { get; set; } = null!;

	public DateTime CreatedTimestamp { get; set; }

	public IEnumerable<CardView> Cards { get; set; } = new List<CardView>();
}