using Luna.Models.Tasks.Domain.Card;

namespace Luna.Models.Tasks.Domain.Page;

public class PageDomain
{
	public Guid Id { get; set; }

	public String Name { get; set; } = null!;

	public String Description { get; set; } = null!;

	public String HeaderImage { get; set; } = null!;

	public DateTime CreatedTimestamp { get; set; }

	public Guid CreatedUserId { get; set; }

	public Guid WorkspaceId { get; set; }

	public IEnumerable<CardDomain> Cards { get; set; } = new List<CardDomain>();
}