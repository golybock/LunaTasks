using Luna.Models.Tasks.Database.Page;

namespace Luna.Tasks.Repositories.Repositories.Page;

public interface IPageRepository
{
	public Task<IEnumerable<PageDatabase>> GetWorkspacePagesAsync(Guid workspaceId, Boolean deleted = false);

	public Task<IEnumerable<PageDatabase>> GetPagesByUserAsync(Guid userId, Boolean deleted= false);

	public Task<PageDatabase?> GetPageAsync(Guid id);

	public Task<Boolean> CreatePageAsync(PageDatabase page);

	public Task<Boolean> UpdatePageAsync(Guid id, PageDatabase page);

	public Task<Boolean> ToTrashPageAsync(Guid id);

	public Task<Boolean> DeletePageAsync(Guid id);

	public Task<Boolean> DeleteWorkspacePagesAsync(Guid workspaceId);
}