using Luna.Models.Users.Blank.Users;
using Luna.Models.Users.View.Users;

namespace Luna.Users.Services.Services;

public interface IUserService
{
	public Task<UserView?> GetUserAsync(Guid id);

	public Task<UserView?> GetUserByPhoneOrEmailAsync(string value);

	public Task<IEnumerable<UserView>> GetUsersAsync();

	// limit(50) and get like pages (1-50, 51-101 and etc)
	public Task<IEnumerable<UserView>> GetUsersAsync(int offset);

	public Task<Guid> CreateUserAsync(UserBlank userBlank);

	public Task<Boolean> UpdateUserAsync(Guid id, UserBlank userBlank);

	public Task<Boolean> DeleteUserAsync(Guid id);

	public Task<Boolean> DeleteUserAsync(string username);
}