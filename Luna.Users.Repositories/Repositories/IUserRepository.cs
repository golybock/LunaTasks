using Luna.Models.Users.Database.Users;

namespace Luna.Users.Repositories.Repositories;

public interface IUserRepository
{
	public Task<UserDatabase?> GetUserAsync(Guid id);

	public Task<UserDatabase?> GetUserByPhoneOrEmailAsync(string value);

	public Task<IEnumerable<UserDatabase>> GetUsers();

	// limit(50) and get like pages (1-50, 51-101 and etc)
	public Task<IEnumerable<UserDatabase>> GetUsers(int offset);

	public Task<Boolean> CreateUserAsync(UserDatabase userDatabase);

	public Task<Boolean> UpdateUserAsync(Guid id, UserDatabase userDatabase);

	public Task<Boolean> DeleteUserAsync(Guid id);

	public Task<Boolean> DeleteUserAsync(string username);
}