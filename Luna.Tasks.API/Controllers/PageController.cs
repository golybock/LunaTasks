using Luna.Models.Tasks.Blank.Page;
using Luna.Models.Tasks.View.Page;
using Luna.Tasks.Services.Services.Page;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ControllerBase = Luna.Tools.Web.ControllerBase;

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
	public async Task<IEnumerable<PageView>> GetWorkspacePages(Guid workspaceId, Boolean deleted = false)
	{
		return await _pageService.GetWorkspacePagesAsync(workspaceId, deleted);
	}

	[HttpGet("[action]")]
	public async Task<PageView?> GetPage(Guid id)
	{
		return await _pageService.GetPageAsync(id);
	}

	[HttpPost("[action]")]
	public async Task<IActionResult> CreatePage(PageBlank page)
	{
		return await _pageService.CreatePageAsync(page, UserId);
	}

	[HttpPut("[action]")]
	public async Task<IActionResult> UpdatePage(Guid id, PageBlank page)
	{
		return await _pageService.UpdatePageAsync(id, page, UserId);
	}

	[HttpDelete("[action]")]
	public async Task<IActionResult> TrashPageAsync(Guid id)
	{
		return await _pageService.ToTrashPageAsync(id, UserId);
	}

	[HttpDelete("[action]")]
	public async Task<IActionResult> DeletePage(Guid id)
	{
		return await _pageService.DeletePageAsync(id, UserId);
	}
}