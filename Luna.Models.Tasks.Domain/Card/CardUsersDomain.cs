namespace Luna.Models.Tasks.Domain.Card;

public class CardUsersDomain
{
	public Int32 Id { get; set; }

	public Guid CardId { get; set; }

	public Guid UserId { get; set; }

	public Boolean Deleted { get; set; }
}