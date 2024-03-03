using Luna.Users.Grpc;

namespace Luna.SharedDataAccess.Users.Services;

public class UserService : IUserService
{
	public async Task<IEnumerable<UserDatabase>> GetUsersAsync()
	{
		throw new NotImplementedException();
	}

	public async Task<UserDatabase> GetUserAsync(Guid id)
	{
		throw new NotImplementedException();
	}

	public async Task<UserDatabase> GetUserAsync(string phoneOrEmail)
	{
		throw new NotImplementedException();
	}

	public async Task<bool> CreateUserAsync(UserDatabase userDatabase)
	{
		throw new NotImplementedException();
	}

	public async Task<bool> UpdateUserAsync(Guid id, UserDatabase userDatabase)
	{
		throw new NotImplementedException();
	}

	public async Task<bool> DeleteUserAsync(Guid id)
	{
		throw new NotImplementedException();
	}

	public async Task<bool> DeleteUserAsync(string username)
	{
		throw new NotImplementedException();
	}
}