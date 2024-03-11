using Luna.Models.Tasks.Domain.Card;

namespace Luna.Models.Tasks.View.Card;

public class BlockedCardView
{
	public CardView Card { get; set; }

	public String Comment { get; set; }
	public Guid BlockedUserId { get; set; }

	public DateTime StartBlockTimestamp { get; set; }

	public DateTime? EndBlockTimestamp { get; set; }

	public BlockedCardView(BlockedCardDomain blockedCardDomain, CardDomain cardDomain)
	{
		Card = new CardView(cardDomain);
		Comment = blockedCardDomain.Comment;
		BlockedUserId = blockedCardDomain.BlockedUserId;
		StartBlockTimestamp = blockedCardDomain.StartBlockTimestamp;
		EndBlockTimestamp = blockedCardDomain.EndBlockTimestamp;
	}

	public BlockedCardView(BlockedCardDomain blockedCardDomain, CardView cardView)
	{
		Card = cardView;
		Comment = blockedCardDomain.Comment;
		BlockedUserId = blockedCardDomain.BlockedUserId;
		StartBlockTimestamp = blockedCardDomain.StartBlockTimestamp;
		EndBlockTimestamp = blockedCardDomain.EndBlockTimestamp;
	}
}