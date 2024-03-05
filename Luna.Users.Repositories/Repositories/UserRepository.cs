using Luna.Models.Users.Database.Users;
using Npgsql;
using Npgsql.Extension.Options;
using Npgsql.Extension.Repositories;

namespace Luna.Users.Repositories.Repositories;

public class UserRepository : NpgsqlRepository, IUserRepository
{
	private const string TableName = "users";

	public UserRepository(IDatabaseOptions databaseOptions) : base(databaseOptions) { }

	public async Task<UserDatabase?> GetUserAsync(Guid id)
	{
		var query = $"select * from {TableName} where id = $1";

		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter() {Value = id}
		};

		return await GetAsync<UserDatabase>(query, parameters);
	}

	public async Task<UserDatabase?> GetUserByPhoneOrEmailAsync(string value)
	{
		var query = $"select * from {TableName} where phone_number = $1 or email = $1";

		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter() {Value = value}
		};

		return await GetAsync<UserDatabase>(query, parameters);
	}

	public async Task<IEnumerable<UserDatabase>> GetUsers()
	{
		var query = $"select * from {TableName}";

		return await GetListAsync<UserDatabase>(query);
	}

	public async Task<IEnumerable<UserDatabase>> GetUsers(int offset)
	{
		var query = $"select * from {TableName} limit 50 offset $1";

		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter() {Value = offset}
		};

		return await GetListAsync<UserDatabase>(query, parameters);
	}

	public async Task<bool> CreateUserAsync(UserDatabase userDatabase)
	{
		var query = $"insert into {TableName}(id, username, email, phone_number, email_confirmed) " +
		            "values ($1, $2, $3, $4, $5)";

		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter() {Value = userDatabase.Id},
			new NpgsqlParameter() {Value = userDatabase.Username},
			new NpgsqlParameter() {Value = userDatabase.Email},
			new NpgsqlParameter() {Value = userDatabase.PhoneNumber == null ? DBNull.Value : userDatabase.PhoneNumber},
			new NpgsqlParameter() {Value = userDatabase.EmailConfirmed},
		};

		return await ExecuteAsync(query, parameters);
	}

	public async Task<bool> UpdateUserAsync(Guid id, UserDatabase userDatabase)
	{
		var query = $"update {TableName} set email = $2, phone_number =$3, email_confirmed =$4 where id = $1";

		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter() {Value = id},
			new NpgsqlParameter() {Value = userDatabase.Email},
			new NpgsqlParameter() {Value = userDatabase.PhoneNumber == null ? DBNull.Value : userDatabase.PhoneNumber},
			new NpgsqlParameter() {Value = userDatabase.EmailConfirmed},
		};

		return await ExecuteAsync(query, parameters);
	}

	public async Task<bool> DeleteUserAsync(Guid id)
	{
		return await DeleteAsync(TableName, "id", id);
	}

	public async Task<bool> DeleteUserAsync(string username)
	{
		return await DeleteAsync(TableName, "username", username);
	}
}