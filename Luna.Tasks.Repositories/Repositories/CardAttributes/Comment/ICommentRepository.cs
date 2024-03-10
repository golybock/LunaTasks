using Luna.Models.Tasks.Database.CardAttributes;

namespace Luna.Tasks.Repositories.Repositories.CardAttributes.Comment;

public interface ICommentRepository
{
	public Task<IEnumerable<CommentDatabase>> GetCommentsAsync(Guid cardId);

	public Task<IEnumerable<CommentDatabase>> GetUserCommentsAsync(Guid userId);

	public Task<CommentDatabase?> GetCommentAsync(Int32 commentId);

	public Task<Boolean> CreateCommentAsync(CommentDatabase comment);

	public Task<Boolean> UpdateCommentAsync(Int32 id, CommentDatabase comment);

	public Task<Boolean> DeleteCommentAsync(Int32 id);

	public Task<Boolean> DeleteCardCommentsAsync(Guid cardId);

	public Task<Boolean> DeleteUserCommentsAsync(Guid userId);
}