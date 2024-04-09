using Luna.Models.Tasks.Database.Card;

namespace Luna.Tasks.Repositories.Repositories.Card;

public interface ICardRepository
{
	public Task<IEnumerable<CardDatabase>> GetCardsAsync(Guid pageId, Guid userId);

	public Task<IEnumerable<CardDatabase>> GetCardsAsync(Guid pageId, Boolean deleted = false);

	public Task<IEnumerable<CardDatabase>> GetCardsAsync(IEnumerable<Guid> cardIds);
	public Task<IEnumerable<CardDatabase>> GetCardsByWorkspaceAsync(Guid workspaceId);

	public Task<CardDatabase?> GetCardAsync(Guid id);

	public Task<Boolean> CreateCardAsync(CardDatabase card);

	public Task<Boolean> UpdateCardAsync(Guid id, CardDatabase card);

	public Task<Boolean> DeleteCardAsync(Guid id);


	public Task<BlockedCardDatabase?> GetBlockedCardAsync(Guid cardId);

	public Task<IEnumerable<BlockedCardDatabase>> GetBlockedCardsAsync(IEnumerable<Guid> cardIds);

	public Task<Boolean> CreateBlockedCardAsync(BlockedCardDatabase blockedCard);

	public Task<Boolean> UpdateBlockedCardAsync(Guid cardId, BlockedCardDatabase blockedCard);

	public Task<Boolean> DeleteBlockedCardAsync(Guid cardId);


	public Task<IEnumerable<CardStatusDatabase>> GetCardStatusesAsync(Guid cardId);

	public Task<IEnumerable<CardStatusDatabase>> GetCardStatusesAsync(IEnumerable<Guid> cardIds);

	public Task<CardStatusDatabase?> GetCardStatusAsync(Guid cardId, Guid statusId);

	public Task<CardStatusDatabase?> GetCurrentCardStatusAsync(Guid cardId);

	public Task<IEnumerable<CardStatusDatabase>> GetCurrentCardStatusAsync(IEnumerable<Guid> cardIds);

	public Task<Boolean> CreateCardStatusAsync(CardStatusDatabase cardStatus);

	public Task<Boolean> DeleteCardStatusAsync(Guid cardId, Guid statusId);

	public Task<Boolean> DeleteCardStatusAsync(Guid cardId);


	public Task<IEnumerable<CardTagsDatabase>> GetCardTagsAsync(Guid cardId);

	public Task<IEnumerable<CardTagsDatabase>> GetCardTagsAsync(IEnumerable<Guid> cardIds);

	public Task<CardTagsDatabase?> GetCardTagAsync(Guid cardId, Guid tagId);

	public Task<CardTagsDatabase?> GetCurrentCardTagAsync(Guid cardId);

	public Task<Boolean> CreateCardTagAsync(CardTagsDatabase cardTag);

	public Task<Boolean> CreateCardTagsAsync(IEnumerable<CardTagsDatabase> cardTags);

	public Task<Boolean> DeleteCardTagAsync(Guid cardId, Guid tagId);

	public Task<Boolean> DeleteCardTagsAsync(Guid cardId);

	public Task<IEnumerable<CardUsersDatabase>> GetCardUsersAsync(Guid cardId);

	public Task<IEnumerable<CardUsersDatabase>> GetCardsUsersAsync(IEnumerable<Guid> cardIds);

	public Task<CardUsersDatabase?> GetCardUserAsync(Guid cardId, Guid userId);

	public Task<Boolean> CreateCardUserAsync(CardUsersDatabase cardUsersDatabase);

	public Task<Boolean> CreateCardUsersAsync(IEnumerable<CardUsersDatabase> cardUsersDatabase);

	public Task<Boolean> DeleteCardUserAsync(Guid cardId, Guid userId);

	public Task<Boolean> DeleteCardUsersAsync(Guid cardId);
}