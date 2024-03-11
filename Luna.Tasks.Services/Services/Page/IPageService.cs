using Luna.Models.Tasks.Blank.Page;
using Luna.Models.Tasks.View.Page;

namespace Luna.Tasks.Services.Services.Page;

public interface IPageService
{
	public Task<IEnumerable<PageView>> GetWorkspacePagesAsync(Guid workspaceId);

	public Task<IEnumerable<PageView>> GetPagesByUserAsync(Guid userId);

	public Task<PageView?> GetPageAsync(Guid id);

	public Task<Boolean> CreatePageAsync(PageBlank page, Guid userId);

	public Task<Boolean> UpdatePageAsync(Guid id, PageBlank page, Guid userId);

	public Task<Boolean> DeletePageAsync(Guid id, Guid userId);

	public Task<Boolean> DeleteWorkspacePagesAsync(Guid workspaceId);
}