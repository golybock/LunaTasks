namespace Luna.SharedModels.Database.Users;

public class UserDatabase
{
	public Guid Id { get; set; }

	public string Username { get; set; } = null!;

	public string Email { get; set; } = null!;

	public string? PhoneNumber { get; set; }

	public DateTime CreatedTimestamp { get; set; }
}