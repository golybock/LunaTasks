using Luna.Models.Auth.Database.Auth;
using Npgsql;
using Npgsql.Extension.Options;
using Npgsql.Extension.Repositories;

namespace Luna.Auth.Repositories.Repositories;

public class UserAuthRepository: NpgsqlRepository, IUserAuthRepository
{
	private const string TableName = "user_auth";

	public UserAuthRepository(IDatabaseOptions databaseOptions) : base(databaseOptions) { }

	public async Task<UserAuthDatabase?> GetAuthUserAsync(Guid userId)
	{
		var query = $"select * from {TableName} where user_id = $1";

		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter() {Value = userId}
		};

		return await GetAsync<UserAuthDatabase>(query, parameters);
	}

	public async Task<UserAuthDatabase?> GetAuthUserByIdAsync(Guid id)
	{
		var query = $"select * from {TableName} where id = $1";

		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter() {Value = id}
		};

		return await GetAsync<UserAuthDatabase>(query, parameters);
	}

	public async Task<bool> CreateAuthUserAsync(UserAuthDatabase userAuthDatabase)
	{
		var query = $"insert into {TableName} (id, user_id, password) values ($1, $2, $3)";

		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter() {Value = userAuthDatabase.Id},
			new NpgsqlParameter() {Value = userAuthDatabase.UserId},
			new NpgsqlParameter() {Value = userAuthDatabase.Password},
		};

		return await ExecuteAsync(query, parameters);
	}

	public async Task<bool> UpdateAuthUserAsync(Guid userId, UserAuthDatabase userAuthDatabase)
	{
		var query = $"update {TableName} set password = $1 where user_id = $1";

		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter() {Value = userId},
			new NpgsqlParameter() {Value = userAuthDatabase.Password},
		};

		return await ExecuteAsync(query, parameters);
	}

	public async Task<bool> UpdateAuthUserByIdAsync(Guid id, UserAuthDatabase userAuthDatabase)
	{
		var query = $"update {TableName} set password = $1 where id = $1";

		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter() {Value = id},
			new NpgsqlParameter() {Value = userAuthDatabase.Password},
		};

		return await ExecuteAsync(query, parameters);
	}

	public Task<bool> DeleteAuthUserByIdAsync(Guid id)
	{
		return DeleteAsync(TableName, "id", id);
	}

	public Task<bool> DeleteAuthUserAsync(Guid userId)
	{
		return DeleteAsync(TableName, "user_id", userId);
	}
}