using Luna.Models.Data.Database.Data;
using Npgsql;
using Npgsql.Extension.Options;
using Npgsql.Extension.Repositories;

namespace Luna.Data.Repositories.Repositories;

public class DataRepository: NpgsqlRepository, IDataRepository
{
	public DataRepository(IDatabaseOptions databaseOptions) : base(databaseOptions) { }

	public async Task<FileDatabase?> GetFileAsync(Guid id)
	{
		var query = "select * from files where id = $1 limit 1";

		var parameters = new[]
		{
			new NpgsqlParameter() {Value = id}
		};

		return await GetAsync<FileDatabase>(query, parameters);
	}

	public async Task<IEnumerable<FileDatabase>> GetFilesAsync(bool deleted = false)
	{
		var query = "select * from files where deleted = $1";

		var parameters = new[]
		{
			new NpgsqlParameter() {Value = deleted}
		};

		return await GetListAsync<FileDatabase>(query, parameters);
	}

	public async Task<IEnumerable<FileDatabase>> GetFilesAsync(Guid workspaceId, bool deleted = false)
	{
		var query = "select * from files where deleted = $1 and workspace_id = $2";

		var parameters = new[]
		{
			new NpgsqlParameter() {Value = deleted},
			new NpgsqlParameter() {Value = workspaceId}
		};

		return await GetListAsync<FileDatabase>(query, parameters);
	}

	public async Task<bool> CreateFileAsyncAsync(FileDatabase fileDatabase)
	{
		var query = "insert into files(id, path, file_type, workspace_id, uploaded_by_user_id) VALUES " +
		            "($1, $2, $3, $4, $5)";

		var parameters = new[]
		{
			new NpgsqlParameter() {Value = fileDatabase.Id},
			new NpgsqlParameter() {Value = fileDatabase.Path},
			new NpgsqlParameter() {Value = (int)fileDatabase.FileType},
			new NpgsqlParameter() {Value = fileDatabase.WorkspaceId},
			new NpgsqlParameter() {Value = fileDatabase.UploadedByUserId}
		};

		return await ExecuteAsync(query, parameters);
	}

	public async Task<bool> UpdateFileAsync(Guid id, FileDatabase fileDatabase)
	{
		var query = "update files set path = $2, file_type = $3, deleted = $4 where id = $1";

		var parameters = new[]
		{
			new NpgsqlParameter() {Value = id},
			new NpgsqlParameter() {Value = fileDatabase.Path},
			new NpgsqlParameter() {Value = (int)fileDatabase.FileType},
			new NpgsqlParameter() {Value = fileDatabase.Deleted}
		};

		return await ExecuteAsync(query, parameters);
	}

	public async Task<bool> DeleteFileAsync(Guid id)
	{
		return await DeleteAsync("files", nameof(id), id);
	}
}