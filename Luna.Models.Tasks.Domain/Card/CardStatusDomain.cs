using Luna.Models.Tasks.Domain.CardAttributes;

namespace Luna.Models.Tasks.Domain.Card;

public class CardStatusDomain
{
	public Int32 Id { get; set; }

	public Guid CardId { get; set; }

	public Guid	StatusId { get; set; }

	public StatusDomain Status { get; set; }

	public DateTime SetTimestamp { get; set; }
}