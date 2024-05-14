using Luna.Models.Tasks.Domain.CardAttributes;
using Luna.Models.Users.View.Users;

namespace Luna.Models.Tasks.View.CardAttributes;

public class CommentView
{
	public Int32 Id { get; set; }

	public UserView User { get; set; }

	public String Comment { get; set; } = null!;

	public String AttachmentUrl { get; set; } = null!;

	public CommentView(CommentDomain commentDomain)
	{
		Id = commentDomain.Id;
		User = new UserView(commentDomain.User);
		Comment = commentDomain.Comment;
		AttachmentUrl = commentDomain.AttachmentUrl;
	}

	public CommentView(CommentDomain commentDomain, UserView userView)
	{
		Id = commentDomain.Id;
		User = userView;
		Comment = commentDomain.Comment;
		AttachmentUrl = commentDomain.AttachmentUrl;
	}
}
