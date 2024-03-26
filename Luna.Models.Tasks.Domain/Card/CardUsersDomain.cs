using Luna.Models.Tasks.Database.Card;
using Luna.Models.Users.Domain.Users;

namespace Luna.Models.Tasks.Domain.Card;

public class CardUsersDomain
{
	public Int32 Id { get; set; }

	public Guid CardId { get; set; }

	public Guid UserId { get; set; }

	public UserDomain User { get; set; }

	public Boolean Deleted { get; set; }

	public CardUsersDomain(CardUsersDatabase cardUsersDatabase, UserDomain user)
	{
		Id = cardUsersDatabase.Id;
		CardId = cardUsersDatabase.CardId;
		UserId = cardUsersDatabase.UserId;
		Deleted	= cardUsersDatabase.Deleted;
		User = user;
	}
}