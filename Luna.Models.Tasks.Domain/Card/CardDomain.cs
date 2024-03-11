using Luna.Models.Tasks.Database.Card;
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

	public DateTime? Deadline { get; set; }

	public Guid? PreviousCardId { get; set; }

	public CardDomain? PreviousCard { get; set; }

	public Boolean Deleted { get; set; }

	public BlockedCardDomain? BlockedCard { get; set; }

	public IEnumerable<CommentDomain> Comments { get; set; }

	public IEnumerable<CardTagsDomain> CardTags { get; set; }

	public IEnumerable<CardUsersDomain> Users { get; set; }

	public IEnumerable<CardStatusDomain> Statuses { get; set; }

	public CardDomain
	(
		CardDatabase cardDatabase, TypeDomain typeDomain,
		CardDomain previousCard, BlockedCardDomain blockedCardDomain,
		IEnumerable<CommentDomain> comments, IEnumerable<CardTagsDomain> cardTags,
		IEnumerable<CardUsersDomain> users, IEnumerable<CardStatusDomain> statuses
	)
	{
		Id = cardDatabase.Id;
		Header = cardDatabase.Header;
		Content = cardDatabase.Content;
		Description = cardDatabase.Description;
		CardTypeId = cardDatabase.CardTypeId;
		PageId = cardDatabase.PageId;
		CreatedUserId = cardDatabase.CreatedUserId;
		CreatedTimestamp = cardDatabase.CreatedTimestamp;
		Deadline = cardDatabase.Deadline;
		PreviousCardId = cardDatabase.PreviousCardId;
		Deleted = cardDatabase.Deleted;

		CardType = typeDomain;
		PreviousCard = previousCard;
		BlockedCard = blockedCardDomain;
		Comments = comments;
		CardTags = cardTags;
		Users = users;
		Statuses = statuses;
	}
}