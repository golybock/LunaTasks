using Luna.Models.Auth.Database.Auth;

namespace Luna.Models.Auth.Domain.Auth;

public class UserAuthDomain
{
	public Guid Id { get; set; }

	public Guid UserId { get; set; }

	private Byte[] Password { get; set; }

	public Boolean PasswordIsValid(Byte[] password)
	{
		return Password.SequenceEqual(password);
	}

	public UserAuthDomain(UserAuthDatabase userAuthDatabase)
	{
		Id = userAuthDatabase.Id;
		UserId = userAuthDatabase.UserId;
		Password = userAuthDatabase.Password;
	}
}