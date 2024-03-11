namespace Luna.Models.Tasks.Database.Card;

public class CardDatabase
{
	public Guid Id { get; set; }

	public String Header { get; set; } = null!;

	public String? Content { get; set; }

	public String? Description { get; set; }

	public Guid CardTypeId { get; set; }

	public Guid PageId { get; set; }

	public Guid CreatedUserId { get; set; }

	public DateTime CreatedTimestamp { get; set; }

	public DateTime? Deadline { get; set; }

	public Guid? PreviousCardId { get; set; }

	public Boolean Deleted { get; set; }
}