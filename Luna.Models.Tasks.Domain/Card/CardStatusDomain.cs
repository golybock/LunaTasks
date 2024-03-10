using Luna.Models.Tasks.Database.Card;
using Luna.Models.Tasks.Database.CardAttributes;
using Luna.Models.Tasks.Domain.CardAttributes;

namespace Luna.Models.Tasks.Domain.Card;

public class CardStatusDomain
{
	public Int32 Id { get; set; }

	public Guid CardId { get; set; }

	public Guid	StatusId { get; set; }

	public StatusDomain Status { get; set; }

	public DateTime SetTimestamp { get; set; }

	public CardStatusDomain(CardStatusDatabase cardStatusDatabase, StatusDomain statusDomain)
	{
		Id = cardStatusDatabase.Id;
		CardId = cardStatusDatabase.CardId;
		StatusId = cardStatusDatabase.StatusId;
		Status = statusDomain;
		SetTimestamp = cardStatusDatabase.SetTimestamp;
	}

	public CardStatusDomain(CardStatusDatabase cardStatusDatabase, StatusDatabase statusDatabase)
	{
		Id = cardStatusDatabase.Id;
		CardId = cardStatusDatabase.CardId;
		StatusId = cardStatusDatabase.StatusId;
		Status = new StatusDomain(statusDatabase);
		SetTimestamp = cardStatusDatabase.SetTimestamp;
	}
}