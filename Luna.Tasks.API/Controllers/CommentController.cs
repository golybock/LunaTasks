using Luna.Models.Tasks.Blank.CardAttributes;
using Luna.Models.Tasks.View.CardAttributes;
using Luna.Tasks.Services.Services.CardAttributes.Comment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ControllerBase = Luna.Tools.Web.ControllerBase;

namespace Luna.Tasks.API.Controllers;

[Authorize]
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
	public async Task<IEnumerable<CommentView>> GetUserCommentsAsync()
	{
		return await _commentService.GetUserCommentsAsync(UserId);
	}

	[HttpPost("[action]")]
	public async Task<IActionResult> CreateCommentAsync(CommentBlank comment, Guid userId)
	{
		var result = await _commentService.CreateCommentAsync(comment, userId);

		return result ? Ok() : BadRequest();
	}

	[HttpPut("[action]")]
	public async Task<IActionResult> UpdateCommentAsync(int id, CommentBlank comment)
	{
		var result = await _commentService.UpdateCommentAsync(id, comment, UserId);

		return result ? Ok() : BadRequest();
	}

	[HttpDelete("[action]")]
	public async Task<IActionResult> DeleteCommentAsync(int id)
	{
		var result = await _commentService.DeleteCommentAsync(id);

		return result ? Ok() : BadRequest();
	}
}