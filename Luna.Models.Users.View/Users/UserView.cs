using Luna.Models.Users.Domain.Users;

namespace Luna.Models.Users.View.Users;

public class UserView
{
	public Guid Id { get; }

	public string Username { get; }

	public string Email { get; }

	public string? PhoneNumber { get; }

	public DateTime CreatedTimestamp { get; }

	public bool EmailConfirmed { get; }

	public UserView(Guid id, string username, string email, string? phoneNumber, DateTime createdTimestamp, bool emailConfirmed)
	{
		Id = id;
		Username = username;
		Email = email;
		PhoneNumber = phoneNumber;
		CreatedTimestamp = createdTimestamp;
		EmailConfirmed = emailConfirmed;
	}

	public UserView(UserDomain userDomain)
	{
		Id = userDomain.Id;
		Username = userDomain.Username;
		Email = userDomain.Email;
		PhoneNumber = userDomain.PhoneNumber;
		CreatedTimestamp = userDomain.CreatedTimestamp;
		EmailConfirmed = userDomain.EmailConfirmed;
	}
}