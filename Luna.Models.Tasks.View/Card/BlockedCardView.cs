using Luna.Models.Tasks.Domain.Card;

namespace Luna.Models.Tasks.View.Card;

public class BlockedCardView
{
	public String Comment { get; set; }
	public Guid BlockedUserId { get; set; }

	public DateTime StartBlockTimestamp { get; set; }

	public DateTime? EndBlockTimestamp { get; set; }

	public BlockedCardView(BlockedCardDomain blockedCardDomain)
	{
		Comment = blockedCardDomain.Comment;
		BlockedUserId = blockedCardDomain.BlockedUserId;
		StartBlockTimestamp = blockedCardDomain.StartBlockTimestamp;
		EndBlockTimestamp = blockedCardDomain.EndBlockTimestamp;
	}
}