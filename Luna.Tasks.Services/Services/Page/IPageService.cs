using Luna.Models.Tasks.Blank.Page;
using Luna.Models.Tasks.View.Page;
using Microsoft.AspNetCore.Mvc;

namespace Luna.Tasks.Services.Services.Page;

public interface IPageService
{
	public Task<IEnumerable<PageView>> GetWorkspacePagesAsync(Guid workspaceId);

	public Task<IEnumerable<PageView>> GetPagesByUserAsync(Guid userId);

	public Task<PageView?> GetPageAsync(Guid id);

	public Task<IActionResult> CreatePageAsync(PageBlank page, Guid userId);

	public Task<IActionResult> UpdatePageAsync(Guid id, PageBlank page, Guid userId);

	public Task<IActionResult> TrashPageAsync(Guid id, Guid userId);

	public Task<IActionResult> DeletePageAsync(Guid id, Guid userId);

	public Task<Boolean> DeleteWorkspacePagesAsync(Guid workspaceId);
}