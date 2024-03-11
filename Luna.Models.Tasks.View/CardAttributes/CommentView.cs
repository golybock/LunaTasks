using Luna.Models.Tasks.Domain.CardAttributes;

namespace Luna.Models.Tasks.View.CardAttributes;

public class CommentView
{
	public Int32 Id { get; set; }

	public Guid UserId { get; set; }

	public String Comment { get; set; } = null!;

	public String AttachmentUrl { get; set; } = null!;

	public CommentView(CommentDomain commentDomain)
	{
		Id = commentDomain.Id;
		UserId = commentDomain.UserId;
		Comment = commentDomain.Comment;
		AttachmentUrl = commentDomain.AttachmentUrl;
	}
}