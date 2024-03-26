using Luna.Models.Users.Domain.Users;
using Luna.Models.Users.View.Users;
using UserBlank = Luna.Models.Users.Blank.Users.UserBlank;

namespace Luna.SharedDataAccess.Users.Services;

public interface IUserService
{
	public Task<IEnumerable<UserView>> GetUsersAsync();

	public Task<UserView?> GetUserAsync(Guid id);

	public Task<UserView?> GetUserAsync(string phoneOrEmail);

	public Task<IEnumerable<UserDomain>> GetUsersDomainAsync();

	public Task<UserDomain?> GetUserDomainAsync(Guid id);

	public Task<UserDomain?> GetUserDomainAsync(string phoneOrEmail);

	public Task<Guid> CreateUserAsync(UserBlank userBlank);

	public Task<bool> UpdateUserAsync(Guid id, UserBlank userBlank);

	public Task<bool> DeleteUserAsync(Guid id);

	public Task<bool> DeleteUserAsync(string username);
}