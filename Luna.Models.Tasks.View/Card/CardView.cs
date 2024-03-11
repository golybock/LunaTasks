using Luna.Models.Tasks.Domain.Card;
using Luna.Models.Tasks.View.CardAttributes;

namespace Luna.Models.Tasks.View.Card;

public class CardView
{
	public Guid Id { get; set; }

	public String Header { get; set; }

	public String Content { get; set; }

	public String Description { get; set; }

	public TypeView CardType { get; set; }

	public Guid CreatedUserId { get; set; }

	public DateTime CreatedTimestamp { get; set; }

	public DateTime? Deadline { get; set; }

	public CardView? PreviousCard { get; set; }

	public IEnumerable<CommentView> Comments { get; set; }

	public IEnumerable<TagView> CardTags { get; set; }

	//todo: add user model
	public IEnumerable<Guid> Users { get; set; }

	public IEnumerable<CardStatusView> Statuses { get; set; }

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
		Users = cardDomain.Users.Select(c => c.UserId).ToList();
		Statuses = cardDomain.Statuses.Select(cardStatusDomain => new CardStatusView(cardStatusDomain));
	}
}