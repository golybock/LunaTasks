namespace Luna.Models.Tasks.Blank.CardAttributes;

public class CommentBlank
{
	public Guid CardId { get; set; }

	public String Comment { get; set; } = null!;

	public String AttachmentUrl { get; set; } = null!;
}