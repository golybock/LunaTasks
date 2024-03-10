using Luna.Models.Tasks.Database.Card;

namespace Luna.Tasks.Repositories.Repositories.Card;

public interface ICardRepository
{
	public Task<IEnumerable<CardDatabase>> GetCardsAsync(Guid workspaceId, Guid userId);

	public Task<IEnumerable<CardDatabase>> GetCardsAsync(Guid workspaceId);

	public Task<IEnumerable<CardDatabase>> GetCardsAsync(IEnumerable<Guid> cardIds);

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

	public Task<CardStatusDatabase?> GetCardStatusAsync(Guid cardId, Guid statusId);

	public Task<CardStatusDatabase?> GetCurrentCardStatusAsync(Guid cardId);

	public Task<Boolean> CreateCardStatusAsync(CardStatusDatabase cardStatus);

	public Task<Boolean> DeleteCardStatusAsync(Guid cardId, Guid statusId);


	public Task<IEnumerable<CardTagsDatabase>> GetCardTagsAsync(Guid cardId);

	public Task<CardTagsDatabase?> GetCardTagAsync(Guid cardId, Guid tagId);

	public Task<CardTagsDatabase?> GetCurrentCardTagAsync(Guid cardId);

	public Task<Boolean> CreateCardTagAsync(CardTagsDatabase cardTag);

	public Task<Boolean> DeleteCardTagAsync(Guid cardId, Guid tagId);


	public Task<IEnumerable<CardUsersDatabase>> GetCardUsersAsync(Guid cardId);

	public Task<CardUsersDatabase?> GetCardUserAsync(Guid cardId, Guid userId);

	public Task<Boolean> CreateCardUsersAsync(CardUsersDatabase cardUsersDatabase);

	public Task<Boolean> DeleteCardUsersAsync(Guid cardId, Guid userId);
}