using Luna.Models.Tasks.Blank.Card;
using Luna.Models.Tasks.Domain.Card;
using Luna.Models.Tasks.View.Card;
using Luna.Models.Tasks.View.CardAttributes;
using Luna.Models.Users.View.Users;

namespace Luna.Tasks.Services.Services.Card;

public interface ICardService
{
	public Task<IEnumerable<CardView>> GetCardsAsync(Guid pageId, Guid userId);

	public Task<IEnumerable<CardView>> GetCardsAsync(Guid pageId);

	public Task<IEnumerable<CardView>> GetCardsAsync(IEnumerable<Guid> cardIds);

	public Task<CardView?> GetCardAsync(Guid id);

	public Task<IEnumerable<CardDomain>> GetCardsDomainAsync(Guid pageId, Guid userId);

	public Task<IEnumerable<CardDomain>> GetCardsDomainAsync(Guid pageId);

	public Task<IEnumerable<CardDomain>> GetCardsPreviewAsync(Guid pageId);

	public Task<IEnumerable<CardDomain>> GetCardsDomainAsync(IEnumerable<Guid> cardIds);

	public Task<CardDomain?> GetCardDomainAsync(Guid id);


	public Task<Boolean> CreateCardAsync(CardBlank card, Guid userId);

	public Task<Boolean> UpdateCardAsync(Guid id, CardBlank card, Guid userId);

	public Task<Boolean> DeleteCardAsync(Guid id, Guid userId);


	public Task<BlockedCardView?> GetBlockedCardAsync(Guid cardId);

	public Task<IEnumerable<BlockedCardView>> GetBlockedCardsAsync(IEnumerable<Guid> cardIds);

	public Task<Boolean> CreateBlockedCardAsync(BlockedCardBlank blockedCard, Guid userId);

	public Task<Boolean> UpdateBlockedCardAsync(Guid cardId, BlockedCardBlank blockedCard, Guid userId);

	public Task<Boolean> DeleteBlockedCardAsync(Guid cardId, Guid userId);


	public Task<IEnumerable<StatusView>> GetCardStatusesAsync(Guid cardId);

	public Task<StatusView?> GetCardStatusAsync(Guid cardId, Guid statusId);

	public Task<StatusView?> GetCurrentCardStatusAsync(Guid cardId);

	public Task<Boolean> CreateCardStatusAsync(Guid cardId, Guid statusId, Guid userId);

	public Task<Boolean> DeleteCardStatusAsync(Guid cardId, Guid statusId, Guid userId);


	public Task<IEnumerable<TagView>> GetCardTagsAsync(Guid cardId);

	public Task<Boolean> CreateCardTagAsync(Guid cardId, Guid tagId, Guid userId);

	public Task<Boolean> DeleteCardTagAsync(Guid cardId, Guid tagId, Guid userId);


	public Task<IEnumerable<UserView>> GetCardUsersAsync(Guid cardId);

	public Task<Boolean> CreateCardUsersAsync(Guid cardId, Guid userId);

	public Task<Boolean> DeleteCardUsersAsync(Guid cardId, Guid userId);
}