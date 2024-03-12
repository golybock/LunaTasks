using Luna.Models.Tasks.Blank.CardAttributes;
using Luna.Models.Tasks.View.CardAttributes;
using Luna.Tasks.Services.Services.CardAttributes.Comment;
using Microsoft.AspNetCore.Mvc;

namespace Luna.Tasks.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentController: ControllerBase
{
	private ICommentService _commentService;

	public CommentController(ICommentService commentService)
	{
		_commentService = commentService;
	}

	[HttpGet("[action]")]
	public async Task<IEnumerable<CommentView>> GetCommentsAsync(Guid cardId)
	{
		return await _commentService.GetCommentsAsync(cardId);
	}

	[HttpGet("[action]")]
	public async Task<IEnumerable<CommentView>> GetUserCommentsAsync(Guid userId)
	{
		return await _commentService.GetUserCommentsAsync(userId);
	}

	[HttpPost("[action]")]
	public async Task<IActionResult> CreateCommentAsync(CommentBlank comment, Guid userId)
	{
		var result = await _commentService.CreateCommentAsync(comment, userId);

		return result ? Ok() : BadRequest();
	}

	[HttpPut("[action]")]
	public async Task<IActionResult> UpdateCommentAsync(int id, CommentBlank comment, Guid userId)
	{
		var result = await _commentService.UpdateCommentAsync(id, comment, userId);

		return result ? Ok() : BadRequest();
	}

	[HttpDelete("[action]")]
	public async Task<IActionResult> DeleteCommentAsync(int id)
	{
		var result = await _commentService.DeleteCommentAsync(id);

		return result ? Ok() : BadRequest();
	}
}