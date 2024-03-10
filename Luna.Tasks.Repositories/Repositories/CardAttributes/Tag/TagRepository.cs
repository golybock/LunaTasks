using Luna.Models.Tasks.Database.CardAttributes;
using Npgsql;
using Npgsql.Extension.Options;
using Npgsql.Extension.Repositories;

namespace Luna.Tasks.Repositories.Repositories.CardAttributes.Tag;

public class TagRepository : NpgsqlRepository, ITagRepository
{
	public TagRepository(IDatabaseOptions options) : base(options)
	{
	}

	public async Task<IEnumerable<TagDatabase>> GetTagsAsync(Guid workspaceId)
	{
		var query = "SELECT * FROM tag WHERE workspace_id = $1";

		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter() {Value = workspaceId}
		};

		return await GetListAsync<TagDatabase>(query, parameters);
	}

	public async Task<TagDatabase?> GetTagAsync(Guid workspaceId, Guid tagId)
	{
		var query = "SELECT * FROM tag WHERE workspace_id = $1 and id = $2";

		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter() {Value = workspaceId},
			new NpgsqlParameter(){Value = tagId}
		};

		return await GetAsync<TagDatabase>(query, parameters);
	}

	public async Task<TagDatabase?> GetTagAsync(Guid tagId)
	{
		var query = "SELECT * FROM tag WHERE id = $1";

		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter(){Value = tagId}
		};

		return await GetAsync<TagDatabase>(query, parameters);
	}

	public async Task<Boolean> CreateTagAsync(TagDatabase tag)
	{
		var query = "INSERT INTO tag (id, name, hex_color, workspace_id, deleted) VALUES ($1, $2, $3, $4, $5)";

		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter() {Value = tag.Id},
			new NpgsqlParameter() {Value = tag.Name},
			new NpgsqlParameter() {Value = tag.HexColor},
			new NpgsqlParameter() {Value = tag.WorkspaceId},
			new NpgsqlParameter() {Value = tag.Deleted}
		};

		return await ExecuteAsync(query, parameters);
	}

	public async Task<Boolean> UpdateTagAsync(Guid id, TagDatabase tag)
	{
		var query = "UPDATE tag SET name = $2, hex_color = $3, deleted = $4 WHERE id = $1";
		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter() {Value = id},
			new NpgsqlParameter() {Value = tag.Name},
			new NpgsqlParameter() {Value = tag.HexColor},
			new NpgsqlParameter() {Value = tag.Deleted},
		};

		return await ExecuteAsync(query, parameters);
	}

	public Task<Boolean> DeleteTagAsync(Guid id)
	{
		return DeleteAsync("tag", nameof(id), id);
	}
}