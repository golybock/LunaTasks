namespace Luna.Models.Tasks.Database.Card;

public class CardUsersDatabase
{
	public Int32 Id { get; set; }

	public Guid CardId { get; set; }

	public Guid UserId { get; set; }

	public Boolean Deleted { get; set; }
}