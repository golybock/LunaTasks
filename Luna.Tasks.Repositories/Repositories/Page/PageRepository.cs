﻿using Luna.Models.Tasks.Database.Page;
using Npgsql;
using Npgsql.Extension.Options;
using Npgsql.Extension.Repositories;

namespace Luna.Tasks.Repositories.Repositories.Page;

public class PageRepository : NpgsqlRepository, IPageRepository
{
	public PageRepository(IDatabaseOptions databaseOptions) : base(databaseOptions)
	{
	}

	public async Task<IEnumerable<PageDatabase>> GetWorkspacePagesAsync(Guid workspaceId, bool deleted = false)
	{
		var query = "SELECT * FROM page WHERE workspace_id = $1 and deleted = $2";

		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter() {Value = workspaceId},
			new NpgsqlParameter() {Value = deleted},
		};

		return await GetListAsync<PageDatabase>(query, parameters);
	}

	public async Task<IEnumerable<PageDatabase>> GetPagesByUserAsync(Guid userId, bool deleted = false)
	{
		var query = "SELECT * FROM page WHERE created_user_id = $1 and deleted = $2";

		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter() {Value = userId},
			new NpgsqlParameter() {Value = deleted},
		};

		return await GetListAsync<PageDatabase>(query, parameters);
	}

	public async Task<PageDatabase?> GetPageAsync(Guid pageId)
	{
		var query = "SELECT * FROM page WHERE id = $1";

		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter() {Value = pageId}
		};

		return await GetAsync<PageDatabase>(query, parameters);
	}

	public async Task<Boolean> CreatePageAsync(PageDatabase page)
	{
		var query = "INSERT INTO page (id, name, description, header_image, created_user_id, workspace_id) " +
		            "VALUES ($1, $2, $3, $4, $5, $6)";

		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter() {Value = page.Id},
			new NpgsqlParameter() {Value = page.Name},
			new NpgsqlParameter() {Value = page.Description},
			new NpgsqlParameter() {Value = page.HeaderImage},
			new NpgsqlParameter() {Value = page.CreatedUserId},
			new NpgsqlParameter() {Value = page.WorkspaceId}
		};

		return await ExecuteAsync(query, parameters);
	}

	public async Task<Boolean> UpdatePageAsync(Guid id, PageDatabase page)
	{
		var query = "UPDATE page SET name = $2, description = $3, header_image = $4, deleted = $5 WHERE id = $1";

		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter() {Value = id},
			new NpgsqlParameter() {Value = page.Name},
			new NpgsqlParameter() {Value = page.Description},
			new NpgsqlParameter() {Value = page.HeaderImage},
			new NpgsqlParameter() {Value = page.Deleted},
		};

		return await ExecuteAsync(query, parameters);
	}

	public async Task<bool> ToTrashPageAsync(Guid id)
	{
		var query = "UPDATE page SET deleted = true WHERE id = $1";

		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter() {Value = id}
		};

		return await ExecuteAsync(query, parameters);
	}

	public Task<Boolean> DeletePageAsync(Guid id)
	{
		return DeleteAsync("page", "id", id);
	}

	public Task<Boolean> DeleteWorkspacePagesAsync(Guid workspaceId)
	{
		return DeleteAsync("page", "workspace_id", workspaceId);
	}
}