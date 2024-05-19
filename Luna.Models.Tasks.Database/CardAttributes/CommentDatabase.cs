namespace Luna.Models.Tasks.Database.CardAttributes;

public class CommentDatabase
{
	public Guid Id { get; set; }

	public Guid CardId { get; set; }

	public Guid UserId { get; set; }

	public String Comment { get; set; } = null!;

	public String AttachmentUrl { get; set; } = null!;

	public Boolean Deleted { get; set; }
}