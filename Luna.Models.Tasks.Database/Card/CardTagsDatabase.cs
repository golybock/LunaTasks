namespace Luna.Models.Tasks.Database.Card;

public class CardTagsDatabase
{
	public Int32 Id { get; set; }

	public Guid CardId { get; set; }

	public Guid TagId { get; set; }

	public Boolean Deleted { get; set; }
}