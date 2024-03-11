using Luna.Models.Tasks.Domain.Card;
using Luna.Models.Tasks.View.CardAttributes;

namespace Luna.Models.Tasks.View.Card;

public class CardStatusView
{
	public StatusView Status { get; set; }

	public DateTime SetTimestamp { get; set; }

	public CardStatusView(CardStatusDomain cardStatusDomain)
	{
		Status = new StatusView(cardStatusDomain.Status);
		SetTimestamp = cardStatusDomain.SetTimestamp;
	}
}