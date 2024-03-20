using Luna.Models.Tasks.Blank.CardAttributes;
using Luna.Models.Tasks.Database.CardAttributes;
using Luna.Models.Tasks.Domain.CardAttributes;
using Luna.Models.Tasks.View.CardAttributes;
using Luna.Tasks.Repositories.Repositories.CardAttributes.Comment;

namespace Luna.Tasks.Services.Services.CardAttributes.Comment;

public class CommentService : ICommentService
{
	private readonly ICommentRepository _commentRepository;

	public CommentService(ICommentRepository commentRepository)
	{
		_commentRepository = commentRepository;
	}

	public async Task<IEnumerable<CommentView>> GetCommentsAsync(Guid cardId)
	{
		var comments = await _commentRepository.GetCommentsAsync(cardId);

		return ToCommentViews(comments);
	}

	public async Task<IEnumerable<CommentView>> GetUserCommentsAsync(Guid userId)
	{
		var comments = await _commentRepository.GetUserCommentsAsync(userId);

		return ToCommentViews(comments);
	}

	public async Task<CommentView?> GetCommentAsync(int commentId)
	{
		var comment = await _commentRepository.GetCommentAsync(commentId);

		if (comment == null)
			return null;

		return ToCommentView(comment);
	}

	public async Task<IEnumerable<CommentDomain>> GetCommentsDomainAsync(Guid cardId)
	{
		var comments = await _commentRepository.GetCommentsAsync(cardId);

		return ToCommentDomains(comments);
	}

	public async Task<IEnumerable<CommentDomain>> GetUserCommentsDomainAsync(Guid userId)
	{
		var comments = await _commentRepository.GetUserCommentsAsync(userId);

		return ToCommentDomains(comments);
	}

	public async Task<CommentDomain?> GetCommentDomainAsync(int commentId)
	{
		var comment = await _commentRepository.GetCommentAsync(commentId);

		if (comment == null)
			return null;

		return ToCommentDomain(comment);
	}

	public async Task<bool> CreateCommentAsync(CommentBlank comment, Guid userId)
	{
		var commentDatabase = ToCommentDatabase(comment, userId);

		return await _commentRepository.CreateCommentAsync(commentDatabase);
	}

	public async Task<bool> UpdateCommentAsync(int id, CommentBlank comment, Guid userId)
	{
		var commentDatabase = ToCommentDatabase(comment, userId);

		return await _commentRepository.UpdateCommentAsync(id, commentDatabase);
	}

	public async Task<bool> DeleteCommentAsync(int id)
	{
		return await _commentRepository.DeleteCommentAsync(id);
	}

	public async Task<bool> DeleteCardCommentsAsync(Guid cardId)
	{
		return await _commentRepository.DeleteCardCommentsAsync(cardId);
	}

	public async Task<bool> DeleteUserCommentsAsync(Guid userId)
	{
		return await _commentRepository.DeleteUserCommentsAsync(userId);
	}

	private CommentDatabase ToCommentDatabase(CommentBlank commentBlank, Guid userId)
	{
		return new CommentDatabase
		{
			CardId = commentBlank.CardId,
			UserId = userId,
			Comment = commentBlank.Comment,
			AttachmentUrl = commentBlank.AttachmentUrl,
			Deleted = false
		};
	}

	private CommentView ToCommentView(CommentDatabase commentDatabase)
	{
		var commentDomain = new CommentDomain(commentDatabase);
		return new CommentView(commentDomain);
	}

	private CommentDomain ToCommentDomain(CommentDatabase commentDatabase)
	{
		return new CommentDomain(commentDatabase);
	}

	private IEnumerable<CommentView> ToCommentViews(IEnumerable<CommentDatabase> commentDatabases)
	{
		return commentDatabases.Select(ToCommentView).ToList();
	}

	private IEnumerable<CommentDomain> ToCommentDomains(IEnumerable<CommentDatabase> commentDatabases)
	{
		return commentDatabases.Select(ToCommentDomain).ToList();
	}
}