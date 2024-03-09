using Luna.Models.Tasks.Domain.CardAttributes;

namespace Luna.Models.Tasks.Domain.Card;

public class CardDomain
{
	public Guid Id { get; set; }

	public String Header { get; set; } = null!;

	public String Content { get; set; } = null!;

	public String Description { get; set; } = null!;

	public Guid CardTypeId { get; set; }

	public TypeDomain CardType { get; set; }

	public Guid PageId { get; set; }

	public Guid CreatedUserId { get; set; }

	public DateTime CreatedTimestamp { get; set; }

	public DateTime Deadline { get; set; }

	public Guid PreviousCardId { get; set; }

	public CardDomain? PreviousCard { get; set; }

	public Boolean Deleted { get; set; }

	public IEnumerable<CommentDomain> Comments { get; set; } = new List<CommentDomain>();

	public IEnumerable<CardTagsDomain> CardTags { get; set; } = new List<CardTagsDomain>();

	public IEnumerable<CardUsersDomain> Users { get; set; } = new List<CardUsersDomain>();

	public IEnumerable<CardStatusDomain> Statuses { get; set; } = new List<CardStatusDomain>();
}