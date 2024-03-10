using Luna.Models.Tasks.Database.CardAttributes;
using Npgsql;
using Npgsql.Extension.Options;
using Npgsql.Extension.Repositories;

namespace Luna.Tasks.Repositories.Repositories.CardAttributes.Status;

public class StatusRepository : NpgsqlRepository, IStatusRepository
{
	public StatusRepository(IDatabaseOptions databaseOptions) : base(databaseOptions) { }

	public async Task<IEnumerable<StatusDatabase>> GetStatusesAsync(Guid workspaceId)
	{
		var query = "SELECT * FROM status WHERE workspace_id = $1";

		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter() {Value = workspaceId}
		};

		return await GetListAsync<StatusDatabase>(query, parameters);
	}

	public async Task<StatusDatabase?> GetStatusAsync(Guid workspaceId, Guid statusId)
	{
		var query = "SELECT * FROM status WHERE workspace_id = $1 AND id = $2";

		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter() {Value = workspaceId},
			new NpgsqlParameter() {Value = statusId}
		};

		return await GetAsync<StatusDatabase>(query, parameters);
	}

	public async Task<StatusDatabase?> GetStatusAsync(Guid statusId)
	{
		var query = "SELECT * FROM status WHERE id = $1";

		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter() {Value = statusId}
		};

		return await GetAsync<StatusDatabase>(query, parameters);
	}

	public async Task<bool> CreateStatusAsync(StatusDatabase status)
	{
		var query = "insert into status(id, name, hex_color, workspace_id) VALUES ($1, $2, $3, $4)";

		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter() {Value = status.Id},
			new NpgsqlParameter() {Value = status.Name},
			new NpgsqlParameter() {Value = status.HexColor},
			new NpgsqlParameter() {Value = status.WorkspaceId}
		};

		return await ExecuteAsync(query, parameters);
	}

	public async Task<bool> UpdateStatusAsync(Guid id, StatusDatabase status)
	{
		var query = "UPDATE status SET name = $2, hex_color = $3 WHERE id = $1";

		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter() {Value = id},
			new NpgsqlParameter() {Value = status.Name},
			new NpgsqlParameter() {Value = status.HexColor},
		};

		return await ExecuteAsync(query, parameters);
	}

	public Task<bool> DeleteStatusAsync(Guid id)
	{
		return DeleteAsync("status", nameof(id), id);
	}
}