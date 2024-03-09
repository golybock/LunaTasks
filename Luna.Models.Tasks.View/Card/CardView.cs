using Luna.Models.Tasks.View.CardAttributes;

namespace Luna.Models.Tasks.View.Card;

public class CardView
{
	public Guid Id { get; set; }

	public String Header { get; set; } = null!;

	public String Content { get; set; } = null!;

	public String Description { get; set; } = null!;

	public TypeView CardType { get; set; }

	public Guid CreatedUserId { get; set; }

	public DateTime CreatedTimestamp { get; set; }

	public DateTime Deadline { get; set; }

	public CardView? PreviousCard { get; set; }

	public IEnumerable<CommentView> Comments { get; set; } = new List<CommentView>();

	public IEnumerable<TagView> CardTags { get; set; } = new List<TagView>();

	//todo: add user model
	public IEnumerable<Guid> Users { get; set; } = new List<Guid>();

	public IEnumerable<CardStatusView> Statuses { get; set; } = new List<CardStatusView>();
}