using Luna.Models.Tasks.Database.Page;

namespace Luna.Tasks.Repositories.Repositories.Page;

public interface IPageRepository
{
	public Task<IEnumerable<PageDatabase>> GetWorkspacePagesAsync(Guid workspaceId);

	public Task<IEnumerable<PageDatabase>> GetPagesByUserAsync(Guid userId);

	public Task<PageDatabase?> GetPageDataAsync(Guid id);

	public Task<Boolean> CreatePageAsync(PageDatabase page);

	public Task<Boolean> UpdatePageAsync(PageDatabase page);

	public Task<Boolean> DeletePageAsync(Guid id);

	public Task<Boolean> DeleteWorkspacePagesAsync(Guid workspaceId);
}