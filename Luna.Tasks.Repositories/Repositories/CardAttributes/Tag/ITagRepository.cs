using Luna.Models.Tasks.Database.CardAttributes;

namespace Luna.Tasks.Repositories.Repositories.CardAttributes.Tag;

public interface ITagRepository
{
	public Task<IEnumerable<TagDatabase>> GetTagsAsync(Guid workspaceId);

	public Task<IEnumerable<TagDatabase>> GetTagsAsync(IEnumerable<Guid> tagIds);

	public Task<TagDatabase?> GetTagAsync(Guid workspaceId, Guid tagId);

	public Task<TagDatabase?> GetTagAsync(Guid tagId);

	public Task<Boolean> CreateTagAsync(TagDatabase tag);

	public Task<Boolean> UpdateTagAsync(Guid id, TagDatabase tag);

	public Task<Boolean> DeleteTagAsync(Guid id);
}