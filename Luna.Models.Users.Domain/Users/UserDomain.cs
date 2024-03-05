using Luna.Models.Users.Database.Users;

namespace Luna.Models.Users.Domain.Users;

public class UserDomain
{
	public Guid Id { get; }

	public string Username { get; }

	public string Email { get; }

	public string? PhoneNumber { get; }

	public DateTime CreatedTimestamp { get; }

	public bool EmailConfirmed { get; }

	public UserDomain(Guid id, string username, string email, string? phoneNumber, DateTime createdTimestamp, bool emailConfirmed)
	{
		Id = id;
		Username = username;
		Email = email;
		PhoneNumber = phoneNumber;
		CreatedTimestamp = createdTimestamp;
		EmailConfirmed = emailConfirmed;
	}

	public UserDomain(UserDatabase userDatabase)
	{
		Id = userDatabase.Id;
		Username = userDatabase.Username;
		Email = userDatabase.Email;
		PhoneNumber = userDatabase.PhoneNumber;
		CreatedTimestamp = userDatabase.CreatedTimestamp;
		EmailConfirmed = userDatabase.EmailConfirmed;
	}
}