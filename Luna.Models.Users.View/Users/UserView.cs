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

	public String? Image { get; set; }


	public UserView(UserDomain userDomain)
	{
		Id = userDomain.Id;
		Username = userDomain.Username;
		Email = userDomain.Email;
		PhoneNumber = userDomain.PhoneNumber;
		CreatedTimestamp = userDomain.CreatedTimestamp;
		EmailConfirmed = userDomain.EmailConfirmed;
		Image = userDomain.Image;
	}
}