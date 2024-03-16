using Luna.Models.Tasks.Blank.Page;
using Luna.Models.Tasks.View.Page;
using Luna.Tasks.Services.Services.Page;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Luna.Tasks.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PageController : ControllerBase
{
	private IPageService _pageService;

	public PageController(IPageService pageService)
	{
		_pageService = pageService;
	}

	[HttpGet("[action]")]
	public async Task<IEnumerable<PageView>> GetWorkspacePagesAsync(Guid workspaceId)
	{
		return await _pageService.GetWorkspacePagesAsync(workspaceId);
	}

	[HttpGet("[action]")]
	public async Task<IEnumerable<PageView>> GetPagesByUserAsync(Guid userId)
	{
		return await _pageService.GetPagesByUserAsync(userId);
	}

	[HttpGet("[action]")]
	public async Task<PageView?> GetPageAsync(Guid id)
	{
		return await _pageService.GetPageAsync(id);
	}

	[HttpPost("[action]")]
	public async Task<IActionResult> CreatePageAsync(PageBlank page, Guid userId)
	{
		var result = await _pageService.CreatePageAsync(page, userId);

		return result ? Ok() : BadRequest();
	}

	[HttpPut("[action]")]
	public async Task<IActionResult> UpdatePageAsync(Guid id, PageBlank page, Guid userId)
	{
		var result = await _pageService.UpdatePageAsync(id, page, userId);

		return result ? Ok() : BadRequest();
	}

	[HttpDelete("[action]")]
	public async Task<IActionResult> DeletePageAsync(Guid id, Guid userId)
	{
		var result = await _pageService.DeletePageAsync(id, userId);

		return result ? Ok() : BadRequest();
	}
}