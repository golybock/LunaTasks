using Luna.Models.Tasks.Database.CardAttributes;
using Luna.Models.Users.Domain.Users;

namespace Luna.Models.Tasks.Domain.CardAttributes;

public class CommentDomain
{
	public Int32 Id { get; set; }

	public Guid CardId { get; set; }

	public Guid UserId { get; set; }

	public UserDomain User { get; set; }

	public String Comment { get; set; } = null!;

	public String AttachmentUrl { get; set; } = null!;

	public Boolean Deleted { get; set; }

	public CommentDomain(CommentDatabase commentDatabase, UserDomain userDomain)
	{
		Id = commentDatabase.Id;
		CardId = commentDatabase.CardId;
		UserId = commentDatabase.UserId;
		User = userDomain;
		Comment = commentDatabase.Comment;
		AttachmentUrl = commentDatabase.AttachmentUrl;
		Deleted = commentDatabase.Deleted;
	}
}