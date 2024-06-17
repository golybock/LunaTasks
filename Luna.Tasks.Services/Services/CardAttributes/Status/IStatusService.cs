using Luna.Models.Tasks.Blank.CardAttributes;
using Luna.Models.Tasks.Domain.CardAttributes;
using Luna.Models.Tasks.View.CardAttributes;

namespace Luna.Tasks.Services.Services.CardAttributes.Status;

public interface IStatusService
{
	public Task<IEnumerable<StatusView>> GetStatusesAsync(Guid workspaceId);

	public Task<IEnumerable<StatusView>> GetStatusesAsync(IEnumerable<Guid> ids);

	public Task<StatusView?> GetStatusAsync(Guid workspaceId, Guid statusId);

	public Task<StatusView?> GetStatusAsync(Guid statusId);


	public Task<IEnumerable<StatusDomain>> GetStatusesDomainAsync(Guid workspaceId);

	public Task<IEnumerable<StatusDomain>> GetStatusesDomainAsync(IEnumerable<Guid> ids);

	public Task<StatusDomain?> GetStatusDomainAsync(Guid workspaceId, Guid statusId);

	public Task<StatusDomain?> GetStatusDomainAsync(Guid statusId);


	public Task<Boolean> CreateStatusAsync(StatusBlank statusBlank, Guid userId);

	public Task<Boolean> UpdateStatusAsync(Guid id, StatusBlank statusBlank, Guid userId);

	public Task<Boolean> DeleteStatusAsync(Guid id, Guid userId);

	public Task<Boolean> TrashStatusAsync(Guid id, Guid userId);
}