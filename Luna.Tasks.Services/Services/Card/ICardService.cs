using Luna.Models.Tasks.Blank.Card;
using Luna.Models.Tasks.View.Card;
using Luna.Models.Tasks.View.CardAttributes;
using Luna.Models.Users.View.Users;

namespace Luna.Tasks.Services.Services.Card;

public interface ICardService
{
	#region card

	public Task<IEnumerable<CardView>> GetCardsByUsersAsync(Guid pageId, List<Guid> userIds);

	public Task<IEnumerable<CardView>> GetCardsByTagsAsync(Guid pageId, List<Guid> tagIds);

	public Task<CardView?> GetCardAsync(Guid id);

	public Task<IEnumerable<CardView>> GetCardsAsync(Guid pageId, Boolean deleted = false);

	public Task<IEnumerable<CardView>> GetCardsByWorkspaceAsync(Guid workspaceId);

	public Task<IEnumerable<CardView>> GetCardsAsync(IEnumerable<Guid> cardIds);

	public Task<Boolean> CreateCardAsync(CardBlank card, Guid userId);

	public Task<Boolean> UpdateCardAsync(Guid id, CardBlank card, Guid userId);

	public Task<Boolean> ToTrashCardAsync(Guid id, Guid userId);

	public Task<Boolean> ToTrashCardsAsync(IEnumerable<Guid> id, Guid userId);

	public Task<Boolean> DeleteCardAsync(Guid id, Guid userId);


	#endregion

	#region blocked card

	protected Task<BlockedCardView?> GetBlockedCardAsync(Guid cardId);

	protected Task<IEnumerable<BlockedCardView>> GetBlockedCardsAsync(IEnumerable<Guid> cardIds);

	public Task<Boolean> CreateBlockedCardAsync(BlockedCardBlank blockedCard, Guid userId);

	public Task<Boolean> UpdateBlockedCardAsync(Guid cardId, BlockedCardBlank blockedCard, Guid userId);

	public Task<Boolean> DeleteBlockedCardAsync(Guid cardId, Guid userId);

	#endregion

	#region card status

	protected Task<IEnumerable<StatusView>> GetCardStatusesAsync(Guid cardId);

	protected Task<StatusView?> GetLastCardStatusAsync(Guid cardId);

	protected Task<Boolean> CreateCardStatusAsync(Guid cardId, Guid statusId);

	protected Task<Boolean> DeleteCardStatusAsync(Guid cardId, Guid statusId);

	protected Task<Boolean> DeleteCardStatusesAsync(Guid cardId);


	#endregion

	#region catd tags

	public Task<IEnumerable<TagView>> GetCardTagsAsync(Guid cardId);

	protected Task<Boolean> CreateCardTagAsync(Guid cardId, Guid tagId);

	protected Task<Boolean> CreateCardTagsAsync(Guid cardId, IEnumerable<Guid> tagIds);

	protected Task<Boolean> UpdateCardTags(Guid cardId, IEnumerable<Guid> tagIds);

	protected Task<Boolean> DeleteCardTagAsync(Guid cardId, Guid tagId);

	protected Task<Boolean> DeleteCardTagsAsync(Guid cardId);

	#endregion

	#region card users

	protected Task<IEnumerable<UserView>> GetCardUsersAsync(Guid cardId);

	protected Task<Boolean> CreateCardUsersAsync(Guid cardId, Guid userId);

	protected Task<Boolean> CreateCardUsersAsync(Guid cardId, IEnumerable<Guid> userIds);

	protected Task<Boolean> UpdateCardUsersAsync(Guid cardId, IEnumerable<Guid> userIds);

	protected Task<Boolean> DeleteCardUsersAsync(Guid cardId, Guid userId);

	protected Task<Boolean> DeleteCardUsersAsync(Guid cardId);

	#endregion

	public Task<Byte[]> GetCardsXlsx(Guid pageId);

	public Task<Byte[]> GetCardDocument(Guid cardId);
}