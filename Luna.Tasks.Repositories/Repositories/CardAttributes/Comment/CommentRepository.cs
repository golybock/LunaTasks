using Luna.Models.Tasks.Database.CardAttributes;
using Npgsql;
using Npgsql.Extension.Options;
using Npgsql.Extension.Repositories;

namespace Luna.Tasks.Repositories.Repositories.CardAttributes.Comment;

public class CommentRepository : NpgsqlRepository, ICommentRepository
{
	public CommentRepository(IDatabaseOptions databaseOptions) : base(databaseOptions)
	{
	}

	public async Task<IEnumerable<CommentDatabase>> GetCommentsAsync(Guid cardId)
	{
		var query = "SELECT * FROM comment WHERE card_id = $1";

		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter() {Value = cardId}
		};

		return await GetListAsync<CommentDatabase>(query, parameters);
	}

	public async Task<IEnumerable<CommentDatabase>> GetCommentsAsync(IEnumerable<Guid> cardIds)
	{
		var query = "SELECT * FROM comment WHERE card_id = any ($1)";

		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter() {Value = cardIds}
		};

		return await GetListAsync<CommentDatabase>(query, parameters);
	}

	public async Task<IEnumerable<CommentDatabase>> GetUserCommentsAsync(Guid userId)
	{
		var query = "SELECT * FROM comment WHERE user_id = $1";

		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter() {Value = userId}
		};

		return await GetListAsync<CommentDatabase>(query, parameters);
	}

	public async Task<CommentDatabase?> GetCommentAsync(int commentId)
	{
		var query = "SELECT * FROM comment WHERE id = $1";

		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter() {Value = commentId}
		};

		return await GetAsync<CommentDatabase>(query, parameters);
	}

	public async Task<bool> CreateCommentAsync(CommentDatabase comment)
	{
		var query = "INSERT INTO comment (id, card_id, user_id, comment, attachment_url, deleted) " +
		            "VALUES ($1, $2, $3, $4, $5, $6)";

		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter() {Value = comment.Id},
			new NpgsqlParameter() {Value = comment.CardId},
			new NpgsqlParameter() {Value = comment.UserId},
			new NpgsqlParameter() {Value = comment.Comment},
			new NpgsqlParameter() {Value = comment.AttachmentUrl},
			new NpgsqlParameter() {Value = comment.Deleted}
		};

		return await ExecuteAsync(query, parameters);
	}

	public async Task<bool> UpdateCommentAsync(int id, CommentDatabase comment)
	{
		var query = "UPDATE comment SET comment = $2, attachment_url = $3, deleted = $4 WHERE id = $1";

		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter() {Value = id},
			new NpgsqlParameter() {Value = comment.Comment},
			new NpgsqlParameter() {Value = comment.AttachmentUrl},
			new NpgsqlParameter() {Value = comment.Deleted},
		};

		return await ExecuteAsync(query, parameters);
	}

	public Task<bool> DeleteCommentAsync(int id)
	{
		return DeleteAsync("comment", "id", id);
	}

	public Task<bool> DeleteCardCommentsAsync(Guid cardId)
	{
		return DeleteAsync("comment", "card_id", cardId);
	}

	public  Task<bool> DeleteUserCommentsAsync(Guid userId)
	{
		return DeleteAsync("comment", "user_id", userId);
	}
}