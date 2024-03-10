using Luna.Models.Tasks.Database.Card;
using Luna.Models.Tasks.Database.CardAttributes;
using Luna.Models.Tasks.Domain.CardAttributes;

namespace Luna.Models.Tasks.Domain.Card;

public class CardTagsDomain
{
	public Int32 Id { get; set; }

	public Guid CardId { get; set; }

	public Guid TagId { get; set; }

	public TagDomain Tag { get; set; }

	public Boolean Deleted { get; set; }

	public CardTagsDomain(CardTagsDatabase cardTagsDatabase, TagDomain tag)
	{
		Id = cardTagsDatabase.Id;
		CardId = cardTagsDatabase.CardId;
		TagId = cardTagsDatabase.TagId;
		Tag = tag;
		Deleted = cardTagsDatabase.Deleted;
	}

	public CardTagsDomain(CardTagsDatabase cardTagsDatabase, TagDatabase tag)
	{
		Id = cardTagsDatabase.Id;
		CardId = cardTagsDatabase.CardId;
		TagId = cardTagsDatabase.TagId;
		Tag = new TagDomain(tag);
		Deleted = cardTagsDatabase.Deleted;
	}
}