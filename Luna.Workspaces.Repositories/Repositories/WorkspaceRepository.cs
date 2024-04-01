using Luna.Models.Workspace.Database.Workspace;
using Npgsql;
using Npgsql.Extension.Options;
using Npgsql.Extension.Repositories;

namespace Luna.Workspaces.Repositories.Repositories;

public class WorkspaceRepository: NpgsqlRepository, IWorkspaceRepository
{
	private const string WorkspaceTableName = "workspace";
	private const string WorkspaceUsersTableName = "workspace_users";

	public WorkspaceRepository(IDatabaseOptions databaseOptions) : base(databaseOptions) { }

	public async Task<IEnumerable<WorkspaceDatabase>> GetWorkspacesAsync()
	{
		var query = $"select * from {WorkspaceTableName}";

		return await GetListAsync<WorkspaceDatabase>(query);
	}

	public async Task<WorkspaceDatabase?> GetWorkspaceAsync(Guid id)
	{
		var query = $"select * from {WorkspaceTableName} where id = $1";

		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter() {Value = id}
		};

		return await GetAsync<WorkspaceDatabase>(query, parameters);
	}

	public async Task<IEnumerable<WorkspaceDatabase>> GetWorkspacesByUserAsync(Guid userId)
	{
		var query = $"select * from {WorkspaceTableName} w join workspace_users wu on " +
		            $"wu.workspace_id = w.id where wu.user_id = $1";

		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter() {Value = userId}
		};

		return await GetListAsync<WorkspaceDatabase>(query, parameters);
	}

	public async Task<IEnumerable<WorkspaceDatabase>> GetWorkspacesByCreatorAsync(Guid userId)
	{
		var query = $"select * from {WorkspaceTableName} where created_user_id = $1";

		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter() {Value = userId}
		};

		return await GetListAsync<WorkspaceDatabase>(query, parameters);
	}

	public async Task<IEnumerable<WorkspaceUsersDatabase>> GetWorkspaceUsersAsync(Guid workspaceId)
	{
		var query = $"select * from {WorkspaceUsersTableName} where workspace_id = $1";

		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter() {Value = workspaceId}
		};

		return await GetListAsync<WorkspaceUsersDatabase>(query, parameters);
	}

	public async Task<bool> CreateWorkspaceAsync(WorkspaceDatabase workspaceDatabase)
	{
		var query = $"insert into {WorkspaceTableName} (id, name,  created_user_id) " +
		            $"values ($1, $2, $3)";

		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter() {Value = workspaceDatabase.Id},
			new NpgsqlParameter() {Value = workspaceDatabase.Name},
			new NpgsqlParameter() {Value = workspaceDatabase.CreatedUserId}
		};

		return await ExecuteAsync(query, parameters);
	}

	public async Task<bool> UpdateWorkspaceAsync(Guid id, WorkspaceDatabase workspaceDatabase)
	{
		var query = $"update {WorkspaceTableName} set name = $2 where id = $1";

		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter() {Value = workspaceDatabase.Id},
			new NpgsqlParameter() {Value = workspaceDatabase.Name},
		};

		return await ExecuteAsync(query, parameters);
	}

	public async Task<bool> DeleteWorkspaceAsync(Guid id)
	{
		return await DeleteCascadeAsync(WorkspaceTableName, "id", id);
	}

	public async Task<bool> AddUserToWorkspace(Guid workspaceId, Guid userId)
	{
		var query = $"insert into {WorkspaceUsersTableName} (user_id, workspace_id) values ($1, $2)";

		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter() {Value = userId},
			new NpgsqlParameter() {Value = workspaceId},
		};

		return await ExecuteAsync(query, parameters);
	}

	public async Task<bool> DeleteUserFromWorkspace(Guid workspaceId, Guid userId)
	{
		var query = $"delete from {WorkspaceUsersTableName} where workspace_id = $1 and user_id = $2";

		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter() {Value = workspaceId},
			new NpgsqlParameter() {Value = userId}
		};

		return await ExecuteAsync(query, parameters);
	}

	public async Task<bool> DeleteUsersFromWorkspace(Guid workspaceId)
	{
		return await DeleteCascadeAsync(WorkspaceUsersTableName, "workspace_id", workspaceId);
	}

	public async Task<bool> DeleteUserFromWorkspaces(Guid userId)
	{
		return await DeleteCascadeAsync(WorkspaceUsersTableName, "user_id", userId);
	}
}