namespace Luna.Models.Tasks.Blank.Card;

public class BlockedCardBlank
{
	public Guid CardId { get; set; }

	public String Comment { get; set; } = null!;

	public DateTime StartBlockTimestamp { get; set; }
}