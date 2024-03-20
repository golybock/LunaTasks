using Luna.Models.Tasks.Blank.CardAttributes;
using Luna.Models.Tasks.Database.CardAttributes;
using Luna.Models.Tasks.Domain.CardAttributes;
using Luna.Models.Tasks.View.CardAttributes;
using Luna.Tasks.Repositories.Repositories.CardAttributes.Tag;

namespace Luna.Tasks.Services.Services.CardAttributes.Tag;

public class TagService : ITagService
{
	private readonly ITagRepository _tagRepository;

	public TagService(ITagRepository tagRepository)
	{
		_tagRepository = tagRepository;
	}

	public async Task<IEnumerable<TagView>> GetTagsAsync(Guid workspaceId)
	{
		var tags = await _tagRepository.GetTagsAsync(workspaceId);

		return ToTagViews(tags);
	}

	public async Task<IEnumerable<TagView>> GetTagsAsync(IEnumerable<Guid> tagIds)
	{
		var tags = await _tagRepository.GetTagsAsync(tagIds);

		return ToTagViews(tags);
	}

	public async Task<TagView?> GetTagAsync(Guid workspaceId, Guid tagId)
	{
		var tag = await _tagRepository.GetTagAsync(workspaceId, tagId);

		if (tag == null)
			return null;

		return ToTagView(tag);
	}

	public async Task<TagView?> GetTagAsync(Guid tagId)
	{
		var tag = await _tagRepository.GetTagAsync(tagId);

		if (tag == null)
			return null;

		return ToTagView(tag);
	}

	public async Task<IEnumerable<TagDomain>> GetTagsDomainAsync(Guid workspaceId)
	{
		var tags = await _tagRepository.GetTagsAsync(workspaceId);

		return ToTagDomains(tags);
	}

	public async Task<IEnumerable<TagDomain>> GetTagsDomainAsync(IEnumerable<Guid> tagIds)
	{
		var tags = await _tagRepository.GetTagsAsync(tagIds);

		return ToTagDomains(tags);
	}

	public async Task<TagDomain?> GetTagDomainAsync(Guid workspaceId, Guid tagId)
	{
		var tag = await _tagRepository.GetTagAsync(workspaceId, tagId);

		if (tag == null)
			return null;

		return ToTagDomain(tag);
	}

	public async Task<TagDomain?> GetTagDomainAsync(Guid tagId)
	{
		var tag = await _tagRepository.GetTagAsync(tagId);

		if (tag == null)
			return null;

		return ToTagDomain(tag);
	}

	public async Task<bool> CreateTagAsync(TagBlank tag, Guid userId)
	{
		var tagDatabase = ToTagDatabase(tag);

		var result = await _tagRepository.CreateTagAsync(tagDatabase);

		return result;
	}

	public async Task<bool> UpdateTagAsync(Guid id, TagBlank tag)
	{
		var tadDatabase = ToTagDatabase(tag);

		var result = await _tagRepository.UpdateTagAsync(id, tadDatabase);

		return result;
	}

	public async Task<bool> DeleteTagAsync(Guid id)
	{
		var result = await _tagRepository.DeleteTagAsync(id);

        return result;
	}

	private TagDatabase ToTagDatabase(TagBlank tag)
	{
		return new TagDatabase
		{
			Id = Guid.NewGuid(),
			WorkspaceId = tag.WorkspaceId,
			Name = tag.Name,
			HexColor = tag.HexColor,
			Deleted = false
		};
	}

	private TagView ToTagView(TagDatabase tag)
	{
		var tagDomain = new TagDomain(tag);
		return new TagView(tagDomain);
	}

	private IEnumerable<TagView> ToTagViews(IEnumerable<TagDatabase> tags)
	{
		return tags.Select(ToTagView).ToList();
	}

	private TagDomain ToTagDomain(TagDatabase tag)
	{
		return new TagDomain(tag);
	}

	private IEnumerable<TagDomain> ToTagDomains(IEnumerable<TagDatabase> tags)
	{
		return tags.Select(ToTagDomain).ToList();
	}
}