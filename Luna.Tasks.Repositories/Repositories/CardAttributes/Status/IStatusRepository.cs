using Luna.Models.Tasks.Database.CardAttributes;

namespace Luna.Tasks.Repositories.Repositories.CardAttributes.Status;

public interface IStatusRepository
{
	public Task<IEnumerable<StatusDatabase>> GetStatusesAsync(Guid workspaceId);

	public Task<IEnumerable<StatusDatabase>> GetStatusesAsync(IEnumerable<Guid> ids);

	public Task<StatusDatabase?> GetStatusAsync(Guid workspaceId, Guid statusId);

	public Task<StatusDatabase?> GetStatusAsync(Guid statusId);

	public Task<Boolean> CreateStatusAsync(StatusDatabase status);

	public Task<Boolean> UpdateStatusAsync(Guid id, StatusDatabase status);

	public Task<Boolean> DeleteStatusAsync(Guid id);
}