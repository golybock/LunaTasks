namespace Luna.Models.Tasks.View.Card;

public class BlockedCardView
{
	public CardView Card { get; set; }

	public String Comment { get; set; } = null!;

	public Guid BlockedUserId { get; set; }

	public DateTime StartBlockTimestamp { get; set; }

	public DateTime EndBlockTimestamp { get; set; }
}