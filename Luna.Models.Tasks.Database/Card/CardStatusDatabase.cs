namespace Luna.Models.Tasks.Database.Card;

public class CardStatusDatabase
{
	public Int32 Id { get; set; }

	public Guid CardId { get; set; }

	public Guid	StatusId { get; set; }

	public DateTime SetTimestamp { get; set; }
}