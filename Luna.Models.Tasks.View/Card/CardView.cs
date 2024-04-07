using Luna.Models.Tasks.Domain.Card;
using Luna.Models.Tasks.View.CardAttributes;
using Luna.Models.Users.View.Users;

namespace Luna.Models.Tasks.View.Card;

public class CardView
{
	public Guid Id { get; set; }

	public String Header { get; set; }

	public String? Content { get; set; }

	public String? Description { get; set; }

	public TypeView CardType { get; set; }

	public Guid CreatedUserId { get; set; }

	public DateTime CreatedTimestamp { get; set; }

	public DateTime? Deadline { get; set; }

	public CardView? PreviousCard { get; set; }

	public IEnumerable<CommentView> Comments { get; set; }

	public IEnumerable<TagView> CardTags { get; set; }

	//todo: add user model
	public IEnumerable<UserView> Users { get; set; }

	public StatusView Status { get; set; }

	public CardView(CardDomain cardDomain)
	{
		Id = cardDomain.Id;
		Header = cardDomain.Header;
		Content = cardDomain.Content;
		Description = cardDomain.Description;
		CardType = new TypeView(cardDomain.CardType);
		CreatedUserId = cardDomain.CreatedUserId;
		CreatedTimestamp = cardDomain.CreatedTimestamp;
		Deadline = cardDomain.Deadline;
		PreviousCard = cardDomain.PreviousCard == null ? null : new CardView(cardDomain.PreviousCard);
		Comments = cardDomain.Comments.Select(commentDomain => new CommentView(commentDomain));
		CardTags = cardDomain.CardTags.Select(tagDomain => new TagView(tagDomain.Tag));
		Users = cardDomain.Users.Select(c => new UserView(c.User)).ToList();
		Status = new StatusView(cardDomain.Status.Status);
	}

	public CardView
	(
		CardDomain cardDomain,
		TypeView cardType,
		IEnumerable<CommentView> comments,
		IEnumerable<TagView> tags,
		IEnumerable<UserView> users,
		StatusView status
	)
	{
		Id = cardDomain.Id;
		Header = cardDomain.Header;
		Content = cardDomain.Content;
		Description = cardDomain.Description;
		CreatedUserId = cardDomain.CreatedUserId;
		CreatedTimestamp = cardDomain.CreatedTimestamp;
		Deadline = cardDomain.Deadline;

		CardType = cardType;
		Comments = comments;
		CardTags = tags;
		Users = users;
		Status = status;
	}
}