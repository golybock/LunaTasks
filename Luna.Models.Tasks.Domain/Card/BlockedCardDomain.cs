using Luna.Models.Tasks.Database.Card;

namespace Luna.Models.Tasks.Domain.Card;

public class BlockedCardDomain
{
	public Int32 Id { get; set; }

	public String Comment { get; set; }

	public Guid BlockedUserId { get; set; }

	public DateTime StartBlockTimestamp { get; set; }

	public DateTime? EndBlockTimestamp { get; set; }

	public BlockedCardDomain(BlockedCardDatabase blockedCardDatabase)
	{
		Id = blockedCardDatabase.Id;
		Comment = blockedCardDatabase.Comment;
		BlockedUserId = blockedCardDatabase.BlockedUserId;
		StartBlockTimestamp = blockedCardDatabase.StartBlockTimestamp;
		EndBlockTimestamp = blockedCardDatabase.EndBlockTimestamp;
	}
}