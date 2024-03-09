namespace Luna.Models.Tasks.Domain.Card;

public class BlockedCardDomain
{
	public Int32 Id { get; set; }

	public Guid CardId { get; set; }

	public CardDomain Card { get; set; }

	public String Comment { get; set; } = null!;

	public Guid BlockedUserId { get; set; }

	public DateTime StartBlockTimestamp { get; set; }

	public DateTime EndBlockTimestamp { get; set; }
}