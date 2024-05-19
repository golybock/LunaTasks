using Luna.Models.Tasks.Blank.CardAttributes;
using Luna.Models.Tasks.Domain.CardAttributes;
using Luna.Models.Tasks.View.CardAttributes;
using Microsoft.AspNetCore.Mvc;

namespace Luna.Tasks.Services.Services.CardAttributes.Comment;

public interface ICommentService
{
	public Task<IEnumerable<CommentView>> GetCommentsAsync(Guid cardId);

	public Task<IEnumerable<CommentView>> GetCommentsAsync(IEnumerable<Guid> cardIds);

	public Task<IEnumerable<CommentView>> GetUserCommentsAsync(Guid userId);

	public Task<CommentView?> GetCommentAsync(Guid commentId);

	public Task<IEnumerable<CommentDomain>> GetCommentsDomainAsync(Guid cardId);

	public Task<IEnumerable<CommentDomain>> GetCommentsDomainAsync(IEnumerable<Guid> cardIds);

	public Task<IEnumerable<CommentDomain>> GetUserCommentsDomainAsync(Guid userId);

	public Task<CommentDomain?> GetCommentDomainAsync(Guid commentId);

	public Task<IActionResult> CreateCommentAsync(CommentBlank comment, Guid userId);

	public Task<IActionResult> UpdateCommentAsync(Guid id, CommentBlank comment, Guid userId);

	public Task<IActionResult> DeleteCommentAsync(Guid id);

	public Task<Boolean> DeleteCardCommentsAsync(Guid cardId);

	public Task<Boolean> DeleteUserCommentsAsync(Guid userId);
}