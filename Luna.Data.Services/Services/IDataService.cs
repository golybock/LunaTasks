using Luna.Models.Data.Domain.Data;
using Microsoft.AspNetCore.Http;

namespace Luna.Data.Services.Services;

public interface IDataService
{
	public Task<Byte[]?> GetFileAsync(Guid id);

	// returns paths to files
	public Task<IEnumerable<String>> GetFilesAsync(Guid userId, Boolean deleted = false);

	// returns paths to files
	public Task<IEnumerable<String>> GetFilesAsync(Guid workspaceId, Guid userId, Boolean deleted = false);

	public Task<Boolean> CreateFileAsyncAsync(IFormFile fileDatabase, Guid workspaceId, Guid userId);

	public Task<Boolean> DeleteFileAsync(Guid id);
}