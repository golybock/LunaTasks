namespace Luna.Models.Tasks.Blank.Card;

public class CardBlank
{
	public String Header { get; set; } = null!;

	public String? Content { get; set; }

	public String? Description { get; set; }

	public Guid CardTypeId { get; set; }

	public Guid PageId { get; set; }

	public DateTime? Deadline { get; set; }

	public Guid? PreviousCardId { get; set; }
}