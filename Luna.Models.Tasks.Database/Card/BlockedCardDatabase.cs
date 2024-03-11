namespace Luna.Models.Tasks.Database.Card;

public class BlockedCardDatabase
{
	public Int32 Id { get; set; }

	public Guid CardId { get; set; }

	public String? Comment { get; set; } = null!;

	public Guid BlockedUserId { get; set; }

	public DateTime StartBlockTimestamp { get; set; }

	public DateTime? EndBlockTimestamp { get; set; }
}