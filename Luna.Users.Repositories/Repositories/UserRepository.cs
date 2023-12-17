using Luna.SharedModels.Database.Users;
using Npgsql.Extension.Options;
using Npgsql.Extension.Repositories;

namespace Luna.Users.Repositories.Repositories;

public class UserRepository : NpgsqlRepository, IUserRepository
{
	public UserRepository(IDatabaseOptions databaseOptions) : base(databaseOptions) { }

	public async Task<UserDatabase?> GetUserAsync(Guid id)
	{
		throw new NotImplementedException();
	}

	public async Task<UserDatabase?> GetUserByPhoneOrEmailAsync(string value)
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
}