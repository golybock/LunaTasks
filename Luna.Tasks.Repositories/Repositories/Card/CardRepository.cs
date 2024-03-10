using Luna.Models.Tasks.Database.Card;
using Npgsql.Extension.Options;
using Npgsql.Extension.Repositories;

namespace Luna.Tasks.Repositories.Repositories.Card;

public class CardRepository : NpgsqlRepository, ICardRepository
{
	public CardRepository(IDatabaseOptions databaseOptions) : base(databaseOptions) { }

	public async Task<IEnumerable<CardDatabase>> GetCardsAsync(Guid workspaceId, Guid userId)
	{
		throw new NotImplementedException();
	}

	public async Task<IEnumerable<CardDatabase>> GetCardsAsync(Guid workspaceId)
	{
		throw new NotImplementedException();
	}

	public async Task<IEnumerable<CardDatabase>> GetCardsAsync(IEnumerable<Guid> cardIds)
	{
		throw new NotImplementedException();
	}

	public async Task<CardDatabase?> GetCardAsync(Guid id)
	{
		throw new NotImplementedException();
	}

	public async Task<bool> CreateCardAsync(CardDatabase card)
	{
		throw new NotImplementedException();
	}

	public async Task<bool> UpdateCardAsync(Guid id, CardDatabase card)
	{
		throw new NotImplementedException();
	}

	public async Task<bool> DeleteCardAsync(Guid id)
	{
		throw new NotImplementedException();
	}

	public async Task<BlockedCardDatabase?> GetBlockedCardAsync(Guid cardId)
	{
		throw new NotImplementedException();
	}

	public async Task<IEnumerable<BlockedCardDatabase>> GetBlockedCardsAsync(IEnumerable<Guid> cardIds)
	{
		throw new NotImplementedException();
	}

	public async Task<bool> CreateBlockedCardAsync(BlockedCardDatabase blockedCard)
	{
		throw new NotImplementedException();
	}

	public async Task<bool> UpdateBlockedCardAsync(Guid cardId, BlockedCardDatabase blockedCard)
	{
		throw new NotImplementedException();
	}

	public async Task<bool> DeleteBlockedCardAsync(Guid cardId)
	{
		throw new NotImplementedException();
	}

	public async Task<IEnumerable<CardStatusDatabase>> GetCardStatusesAsync(Guid cardId)
	{
		throw new NotImplementedException();
	}

	public async Task<CardStatusDatabase?> GetCardStatusAsync(Guid cardId, Guid statusId)
	{
		throw new NotImplementedException();
	}

	public async Task<CardStatusDatabase?> GetCurrentCardStatusAsync(Guid cardId)
	{
		throw new NotImplementedException();
	}

	public async Task<bool> CreateCardStatusAsync(CardStatusDatabase cardStatus)
	{
		throw new NotImplementedException();
	}

	public async Task<bool> DeleteCardStatusAsync(Guid cardId, Guid statusId)
	{
		throw new NotImplementedException();
	}

	public async Task<IEnumerable<CardTagsDatabase>> GetCardTagsAsync(Guid cardId)
	{
		throw new NotImplementedException();
	}

	public async Task<CardTagsDatabase?> GetCardTagAsync(Guid cardId, Guid tagId)
	{
		throw new NotImplementedException();
	}

	public async Task<CardTagsDatabase?> GetCurrentCardTagAsync(Guid cardId)
	{
		throw new NotImplementedException();
	}

	public async Task<bool> CreateCardTagAsync(CardTagsDatabase cardTag)
	{
		throw new NotImplementedException();
	}

	public async Task<bool> DeleteCardTagAsync(Guid cardId, Guid tagId)
	{
		throw new NotImplementedException();
	}

	public async Task<IEnumerable<CardUsersDatabase>> GetCardUsersAsync(Guid cardId)
	{
		throw new NotImplementedException();
	}

	public async Task<CardUsersDatabase?> GetCardUserAsync(Guid cardId, Guid userId)
	{
		throw new NotImplementedException();
	}

	public async Task<bool> CreateCardUsersAsync(CardUsersDatabase cardUsersDatabase)
	{
		throw new NotImplementedException();
	}

	public async Task<bool> DeleteCardUsersAsync(Guid cardId, Guid userId)
	{
		throw new NotImplementedException();
	}
}