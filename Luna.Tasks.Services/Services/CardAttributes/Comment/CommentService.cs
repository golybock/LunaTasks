using Luna.Models.Tasks.Blank.CardAttributes;
using Luna.Models.Tasks.Database.CardAttributes;
using Luna.Models.Tasks.Domain.CardAttributes;
using Luna.Models.Tasks.View.CardAttributes;
using Luna.Models.Users.Domain.Users;
using Luna.SharedDataAccess.Users.Services;
using Luna.Tasks.Repositories.Repositories.CardAttributes.Comment;
using Microsoft.AspNetCore.Mvc;

namespace Luna.Tasks.Services.Services.CardAttributes.Comment;

public class CommentService : ICommentService
{
	private readonly ICommentRepository _commentRepository;
	private readonly IUserService _userService;

	public CommentService(ICommentRepository commentRepository, IUserService userService)
	{
		_commentRepository = commentRepository;
		_userService = userService;
	}

	public async Task<IEnumerable<CommentView>> GetCommentsAsync(Guid cardId)
	{
		var comments = await _commentRepository.GetCommentsAsync(cardId);

		var userIds = comments.Select(comment => comment.UserId).ToList();

		var users = await _userService.GetUsersDomainAsync();

		return ToCommentViews(comments, users);
	}

	public async Task<IEnumerable<CommentView>> GetCommentsAsync(IEnumerable<Guid> cardIds)
	{
		var comments = await _commentRepository.GetCommentsAsync(cardIds);

		var userIds = comments.Select(comment => comment.UserId).ToList();

		var users = await _userService.GetUsersDomainAsync();

		return ToCommentViews(comments, users);
	}

	public async Task<IEnumerable<CommentView>> GetUserCommentsAsync(Guid userId)
	{
		var comments = await _commentRepository.GetUserCommentsAsync(userId);

		var userIds = comments.Select(comment => comment.UserId).ToList();

		var users = await _userService.GetUsersDomainAsync();

		return ToCommentViews(comments, users);
	}

	public async Task<CommentView?> GetCommentAsync(Guid commentId)
	{
		var comment = await _commentRepository.GetCommentAsync(commentId);

		if (comment == null)
			return null;

		var user = await _userService.GetUserDomainAsync(comment.UserId);

		if (user == null)
			return null;

		return ToCommentView(comment, user);
	}

	public async Task<IEnumerable<CommentDomain>> GetCommentsDomainAsync(Guid cardId)
	{
		var comments = await _commentRepository.GetCommentsAsync(cardId);

		var userIds = comments.Select(comment => comment.UserId).ToList();

		var users = await _userService.GetUsersDomainAsync();

		return ToCommentDomains(comments, users);
	}

	public async Task<IEnumerable<CommentDomain>> GetCommentsDomainAsync(IEnumerable<Guid> cardIds)
	{
		var comments = await _commentRepository.GetCommentsAsync(cardIds);

		var userIds = comments.Select(comment => comment.UserId).ToList();

		var users = await _userService.GetUsersDomainAsync();

		return ToCommentDomains(comments, users);
	}

	public async Task<IEnumerable<CommentDomain>> GetUserCommentsDomainAsync(Guid userId)
	{
		var comments = await _commentRepository.GetUserCommentsAsync(userId);

		var userIds = comments.Select(comment => comment.UserId).ToList();

		var users = await _userService.GetUsersDomainAsync();

		return ToCommentDomains(comments, users);
	}

	public async Task<CommentDomain?> GetCommentDomainAsync(Guid commentId)
	{
		var comment = await _commentRepository.GetCommentAsync(commentId);

		if (comment == null)
			return null;

		var user = await _userService.GetUserDomainAsync(comment.UserId);

		if (user == null)
			return null;

		return ToCommentDomain(comment, user);
	}

	public async Task<IActionResult> CreateCommentAsync(CommentBlank comment, Guid userId)
	{
		var commentDatabase = ToCommentDatabase(comment, userId);

		var res = await _commentRepository.CreateCommentAsync(commentDatabase);

		return res ? new OkResult() : new BadRequestResult();
	}

	public async Task<IActionResult> UpdateCommentAsync(Guid id, CommentBlank comment, Guid userId)
	{
		var commentDatabase = ToCommentDatabase(comment, userId);

		var res = await _commentRepository.UpdateCommentAsync(id, commentDatabase);

		return res ? new OkResult() : new BadRequestResult();
	}

	public async Task<IActionResult> DeleteCommentAsync(Guid id)
	{
		var comment = await _commentRepository.GetCommentAsync(id);

		if (comment == null)
			return new NotFoundResult();

		comment.Deleted = true;

		var res = await _commentRepository.UpdateCommentAsync(id, comment);

		return res ? new OkResult() : new BadRequestResult();
	}

	public async Task<Boolean> DeleteCardCommentsAsync(Guid cardId)
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
			Id = Guid.NewGuid(),
			CardId = commentBlank.CardId,
			UserId = userId,
			Comment = commentBlank.Comment,
			AttachmentUrl = commentBlank.AttachmentUrl,
			Deleted = false
		};
	}

	private CommentView ToCommentView(CommentDatabase commentDatabase, UserDomain userDomain)
	{
		var commentDomain = new CommentDomain(commentDatabase, userDomain);
		return new CommentView(commentDomain);
	}

	private CommentDomain ToCommentDomain(CommentDatabase commentDatabase, UserDomain userDomain)
	{
		return new CommentDomain(commentDatabase, userDomain);
	}

	private IEnumerable<CommentView> ToCommentViews(IEnumerable<CommentDatabase> commentDatabases,
		IEnumerable<UserDomain> userDomains)
	{
		return commentDatabases.Select(c => ToCommentView(c, userDomains.First(user => user.Id == c.UserId))).ToList();
	}

	private IEnumerable<CommentDomain> ToCommentDomains(IEnumerable<CommentDatabase> commentDatabases,
		IEnumerable<UserDomain> userDomains)
	{
		return commentDatabases.Select(c => ToCommentDomain(c, userDomains.First(user => user.Id == c.UserId)))
			.ToList();
	}
}