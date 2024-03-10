using Luna.Models.Tasks.Database.CardAttributes;
using Npgsql;
using Npgsql.Extension.Options;
using Npgsql.Extension.Repositories;

namespace Luna.Tasks.Repositories.Repositories.CardAttributes.Role;

public class RoleRepository : NpgsqlRepository, IRoleRepository
{
	public RoleRepository(IDatabaseOptions databaseOptions) : base(databaseOptions) { }

	public async Task<IEnumerable<RoleDatabase>> GetRolesAsync()
	{
		var query = "SELECT * FROM role";

		return await GetListAsync<RoleDatabase>(query);
	}

	public async Task<RoleDatabase?> GetRoleAsync(int roleId)
	{
		var query = "SELECT * FROM role WHERE id = $1";

		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter() {Value = roleId}
		};

		return await GetAsync<RoleDatabase>(query, parameters);
	}

	public async Task<bool> CreateRoleAsync(RoleDatabase role)
	{
		var query = "INSERT INTO role (name) VALUES ($1)";

		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter() {Value = role.Name}
		};

		return await ExecuteAsync(query, parameters);
	}
}