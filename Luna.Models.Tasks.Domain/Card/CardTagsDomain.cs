using Luna.Models.Tasks.Domain.CardAttributes;

namespace Luna.Models.Tasks.Domain.Card;

public class CardTagsDomain
{
	public Int32 Id { get; set; }

	public Guid CardId { get; set; }

	public Guid TagId { get; set; }

	public TagDomain Tag { get; set; }

	public Boolean Deleted { get; set; }
}