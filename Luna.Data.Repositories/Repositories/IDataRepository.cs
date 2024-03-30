using Luna.Models.Data.Database.Data;

namespace Luna.Data.Repositories.Repositories;

public interface IDataRepository
{
	public Task<FileDatabase?> GetFileAsync(Guid id);

	public Task<IEnumerable<FileDatabase>> GetFilesAsync(Boolean deleted = false);

	public Task<IEnumerable<FileDatabase>> GetFilesAsync(Guid workspaceId, Boolean deleted = false);

	public Task<Boolean> CreateFileAsyncAsync(FileDatabase fileDatabase);

	public Task<Boolean> UpdateFileAsync(Guid id, FileDatabase fileDatabase);

	public Task<Boolean> DeleteFileAsync(Guid id);
}