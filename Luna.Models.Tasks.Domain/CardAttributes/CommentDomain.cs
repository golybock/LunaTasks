namespace Luna.Models.Tasks.Domain.CardAttributes;

public class CommentDomain
{
	public Int32 Id { get; set; }

	public Guid CardId { get; set; }

	public Guid UserId { get; set; }

	public String Comment { get; set; } = null!;

	public String AttachmentUrl { get; set; } = null!;

	public Boolean Deleted { get; set; }
}