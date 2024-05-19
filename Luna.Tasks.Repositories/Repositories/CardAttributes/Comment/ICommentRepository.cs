using Luna.Models.Tasks.Database.CardAttributes;

namespace Luna.Tasks.Repositories.Repositories.CardAttributes.Comment;

public interface ICommentRepository
{
	public Task<IEnumerable<CommentDatabase>> GetCommentsAsync(Guid cardId);

	public Task<IEnumerable<CommentDatabase>> GetCommentsAsync(IEnumerable<Guid> cardIds);

	public Task<IEnumerable<CommentDatabase>> GetUserCommentsAsync(Guid userId);

	public Task<CommentDatabase?> GetCommentAsync(Guid commentId);

	public Task<Boolean> CreateCommentAsync(CommentDatabase comment);

	public Task<Boolean> UpdateCommentAsync(Guid id, CommentDatabase comment);

	public Task<Boolean> DeleteCommentAsync(Guid id);

	public Task<Boolean> DeleteCardCommentsAsync(Guid cardId);

	public Task<Boolean> DeleteUserCommentsAsync(Guid userId);
}