namespace Luna.Models.Auth.Database.Auth;

public class UserAuthDatabase
{
	public Guid Id { get; set; }

	public Guid UserId { get; set; }

	public Byte[] Password { get; set; } = [];
}