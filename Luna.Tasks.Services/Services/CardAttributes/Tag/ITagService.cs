using Luna.Models.Tasks.Blank.CardAttributes;
using Luna.Models.Tasks.Domain.CardAttributes;
using Luna.Models.Tasks.View.CardAttributes;

namespace Luna.Tasks.Services.Services.CardAttributes.Tag;

public interface ITagService
{
	public Task<IEnumerable<TagView>> GetTagsAsync(Guid workspaceId);

	public Task<IEnumerable<TagView>> GetTagsAsync(IEnumerable<Guid> tagIds);

	public Task<TagView?> GetTagAsync(Guid workspaceId, Guid tagId);

	public Task<TagView?> GetTagAsync(Guid tagId);

	public Task<IEnumerable<TagDomain>> GetTagsDomainAsync(Guid workspaceId);

	public Task<IEnumerable<TagDomain>> GetTagsDomainAsync(IEnumerable<Guid> tagIds);

	public Task<TagDomain?> GetTagDomainAsync(Guid workspaceId, Guid tagId);

	public Task<TagDomain?> GetTagDomainAsync(Guid tagId);

	public Task<Boolean> CreateTagAsync(TagBlank tag, Guid userId);

	public Task<Boolean> UpdateTagAsync(Guid id, TagBlank tag);

	public Task<Boolean> DeleteTagAsync(Guid id);
}