namespace Luna.Models.Tasks.Blank.Card;

public class CardBlank
{
	public String Header { get; set; } = null!;

	public String? Content { get; set; }

	public String? Description { get; set; }

	public Guid CardTypeId { get; set; }

	public Guid StatusId { get; set; }

	public IEnumerable<Guid> TagIds { get; set; } = new List<Guid>();

	public IEnumerable<Guid> UserIds { get; set; } = new List<Guid>();

	public Guid PageId { get; set; }

	public DateTime? Deadline { get; set; }

	public Guid? PreviousCardId { get; set; }
}