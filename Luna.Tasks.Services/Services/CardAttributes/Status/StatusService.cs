using Luna.Models.Tasks.Blank.CardAttributes;
using Luna.Models.Tasks.Database.CardAttributes;
using Luna.Models.Tasks.Domain.CardAttributes;
using Luna.Models.Tasks.View.CardAttributes;
using Luna.Tasks.Repositories.Repositories.CardAttributes.Status;

namespace Luna.Tasks.Services.Services.CardAttributes.Status;

public class StatusService : IStatusService
{
	private readonly IStatusRepository _statusRepository;

	public StatusService(IStatusRepository statusRepository)
	{
		_statusRepository = statusRepository;
	}

	public async Task<IEnumerable<StatusView>> GetStatusesAsync(Guid workspaceId)
	{
		var statusDatabases = await _statusRepository.GetStatusesAsync(workspaceId);

		return ToStatusView(statusDatabases);
	}

	public async Task<StatusView?> GetStatusAsync(Guid workspaceId, Guid statusId)
	{
		var status = await _statusRepository.GetStatusAsync(workspaceId, statusId);

		if (status == null)
			return null;

		return ToStatusView(status);
	}

	public async Task<StatusView?> GetStatusAsync(Guid statusId)
	{
		var status = await _statusRepository.GetStatusAsync(statusId);

		if (status == null)
			return null;

		return ToStatusView(status);
	}

	public async Task<bool> CreateStatusAsync(StatusBlank statusBlank, Guid userId)
	{
		var statusDatabase = ToStatusDatabase(statusBlank);

		var result = await _statusRepository.CreateStatusAsync(statusDatabase);

		return result;
	}

	public async Task<bool> UpdateStatusAsync(Guid id, StatusBlank statusBlank, Guid userId)
	{
		var statusDatabase = ToStatusDatabase(statusBlank);

		var result = await _statusRepository.UpdateStatusAsync(id, statusDatabase);

		return result;
	}

	public async Task<bool> DeleteStatusAsync(Guid id, Guid userId)
	{
		var result = await _statusRepository.DeleteStatusAsync(id);

		return result;
	}

	private StatusDatabase ToStatusDatabase(StatusBlank statusBlank)
	{
		return new StatusDatabase()
		{
			Id = Guid.NewGuid(),
			WorkspaceId = statusBlank.WorkspaceId,
			HexColor = statusBlank.HexColor,
			Name = statusBlank.Name,
			Deleted = false
		};
	}

	private StatusView ToStatusView(StatusDatabase statusDatabase)
	{
		var statusDomain = new StatusDomain(statusDatabase);
		return new StatusView(statusDomain);
	}

	private IEnumerable<StatusView> ToStatusView(IEnumerable<StatusDatabase> statusDatabases)
	{
		return statusDatabases.Select(ToStatusView).ToList();
	}
}