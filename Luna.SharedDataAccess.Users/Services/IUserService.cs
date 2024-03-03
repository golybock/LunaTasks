using Luna.Users.Grpc;

namespace Luna.SharedDataAccess.Users.Services;

public interface IUserService
{
	public Task<IEnumerable<UserDatabase>> GetUsersAsync();

	public Task<UserDatabase> GetUserAsync(Guid id);

	public Task<UserDatabase> GetUserAsync(string phoneOrEmail);

	public Task<bool> CreateUserAsync(UserDatabase userDatabase);

	public Task<bool> UpdateUserAsync(Guid id, UserDatabase userDatabase);

	public Task<bool> DeleteUserAsync(Guid id);

	public Task<bool> DeleteUserAsync(string username);
}