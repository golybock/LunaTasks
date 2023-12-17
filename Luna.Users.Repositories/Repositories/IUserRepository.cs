using Luna.SharedModels.Database.Users;

namespace Luna.Users.Repositories.Repositories;

public interface IUserRepository
{
	public Task<UserDatabase?> GetUserAsync(Guid id);

	public Task<UserDatabase?> GetUserByPhoneOrEmailAsync(string value);

	public Task<Boolean> CreateUserAsync(UserDatabase userDatabase);

	public Task<Boolean> UpdateUserAsync(Guid id, UserDatabase userDatabase);

	public Task<Boolean> DeleteUserAsync(Guid id);
}