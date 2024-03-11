using Luna.Models.Tasks.Database.Card;
using Npgsql;
using Npgsql.Extension.Options;
using Npgsql.Extension.Repositories;

namespace Luna.Tasks.Repositories.Repositories.Card;

public class CardRepository : NpgsqlRepository, ICardRepository
{
	public CardRepository(IDatabaseOptions databaseOptions) : base(databaseOptions) { }

	public async Task<IEnumerable<CardDatabase>> GetCardsAsync(Guid pageId, Guid userId)
	{
		var query = "SELECT * from card join public.card_users cu on card.id = cu.card_id " +
		            "where page_id = $1 and cu.user_id = $2";

		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter() {Value = pageId},
			new NpgsqlParameter() {Value = userId}
		};

		return await GetListAsync<CardDatabase>(query, parameters);
	}

	public async Task<IEnumerable<CardDatabase>> GetCardsAsync(Guid pageId)
	{
		var query = "SELECT * from card where page_id = $1";

		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter() {Value = pageId},
		};

		return await GetListAsync<CardDatabase>(query, parameters);
	}

	public async Task<IEnumerable<CardDatabase>> GetCardsAsync(IEnumerable<Guid> cardIds)
	{
		var query = "SELECT * from card where id = any ($1)";

		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter() {Value = cardIds},
		};

		return await GetListAsync<CardDatabase>(query, parameters);
	}

	public async Task<CardDatabase?> GetCardAsync(Guid id)
	{
		var query = "SELECT * from card where id = $1";

		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter() {Value = id},
		};

		return await GetAsync<CardDatabase>(query, parameters);
	}

	public async Task<bool> CreateCardAsync(CardDatabase card)
	{
		var query =
			"INSERT INTO card (id, header, content, description, card_type_id, page_id, created_user_id, deadline, previous_card_id) " +
			"VALUES ($1, $2, $3, $4, $5, $6, $7, $7, $8)";

		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter(){Value = card.Id},
			new NpgsqlParameter(){Value = card.Header},
			new NpgsqlParameter(){Value = card.Content == null ? DBNull.Value : card.Content},
			new NpgsqlParameter(){Value = card.Description == null ? DBNull.Value : card.Description},
			new NpgsqlParameter(){ Value = card.CardTypeId},
			new NpgsqlParameter(){ Value = card.PageId},
			new NpgsqlParameter(){ Value = card.CreatedUserId},
			new NpgsqlParameter(){ Value = card.Deadline == null ? DBNull.Value : card.Deadline},
			new NpgsqlParameter(){ Value = card.PreviousCardId == null ? DBNull.Value : card.PreviousCardId}
		};

		return await ExecuteAsync(query, parameters);
	}

	public async Task<bool> UpdateCardAsync(Guid id, CardDatabase card)
	{
		var query =
			"UPDATE card SET header = $2, content = $3, description = $4, " +
			"card_type_id = $5, deadline = $8, previous_card_id = $9 " +
			"WHERE id = $1";

		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter() {Value = id},
			new NpgsqlParameter() {Value = card.Header},
			new NpgsqlParameter() {Value = card.Content == null ? DBNull.Value : card.Content},
			new NpgsqlParameter() {Value = card.Description == null ? DBNull.Value : card.Description},
			new NpgsqlParameter() {Value = card.CardTypeId},
			new NpgsqlParameter() {Value = card.Deadline == null ? DBNull.Value : card.Deadline},
			new NpgsqlParameter() {Value = card.PreviousCardId == null ? DBNull.Value : card.PreviousCardId}
		};

		return await ExecuteAsync(query, parameters);
	}

	public async Task<bool> DeleteCardAsync(Guid id)
	{
		return await DeleteAsync("card", nameof(id), id);
	}

	public async Task<BlockedCardDatabase?> GetBlockedCardAsync(Guid cardId)
	{
		var query = "SELECT * from block_card where card_id = $1 order by start_block_timestamp limit 1";

		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter() {Value = cardId}
		};

		return await GetAsync<BlockedCardDatabase>(query, parameters);
	}

	public async Task<IEnumerable<BlockedCardDatabase>> GetBlockedCardsAsync(IEnumerable<Guid> cardIds)
	{
		var query = "SELECT * from block_card where card_id = any ($1)";

		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter() {Value = cardIds}
		};

		return await GetListAsync<BlockedCardDatabase>(query, parameters);
	}

	public async Task<bool> CreateBlockedCardAsync(BlockedCardDatabase blockedCard)
	{
		var query = "INSERT INTO block_card (card_id, comment, blocked_user_id, end_block_timestamp) " +
		            "VALUES ($1, $2, $3, $4)";

		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter() {Value = blockedCard.CardId},
			new NpgsqlParameter() {Value = blockedCard.Comment == null ? DBNull.Value : blockedCard.Comment},
			new NpgsqlParameter() {Value = blockedCard.BlockedUserId},
			new NpgsqlParameter() {Value = blockedCard.EndBlockTimestamp == null ? DBNull.Value : blockedCard.EndBlockTimestamp}
		};

		return await ExecuteAsync(query, parameters);
	}

	public async Task<bool> UpdateBlockedCardAsync(Guid cardId, BlockedCardDatabase blockedCard)
	{
		var query = "UPDATE block_card SET comment = $2, end_block_timestamp = $3 " +
		            "WHERE card_id = $1";

		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter() {Value = cardId},
			new NpgsqlParameter() {Value = blockedCard.Comment == null ? DBNull.Value : blockedCard.Comment},
			new NpgsqlParameter() {Value = blockedCard.EndBlockTimestamp == null ? DBNull.Value : blockedCard.EndBlockTimestamp}
		};

		return await ExecuteAsync(query, parameters);
	}

	public async Task<bool> DeleteBlockedCardAsync(Guid cardId)
	{
		return await DeleteAsync("block_card", "card_id", cardId);
	}

	public async Task<IEnumerable<CardStatusDatabase>> GetCardStatusesAsync(Guid cardId)
	{
		var query = "SELECT * from card_status where card_id = $1";

		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter() {Value = cardId}
		};

		return await GetListAsync<CardStatusDatabase>(query, parameters);
	}

	public async Task<CardStatusDatabase?> GetCardStatusAsync(Guid cardId, Guid statusId)
	{
		var query = "SELECT * from card_status where card_id = $1 and status_id = $2";

		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter() {Value = cardId},
			new NpgsqlParameter() {Value = statusId}
		};

		return await GetAsync<CardStatusDatabase>(query, parameters);
	}

	public async Task<CardStatusDatabase?> GetCurrentCardStatusAsync(Guid cardId)
	{
		var query = "SELECT * from card_status where card_id = $1 order by set_timestamp limit 1";

		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter() {Value = cardId}
		};

		return await GetAsync<CardStatusDatabase>(query, parameters);
	}

	public async Task<bool> CreateCardStatusAsync(CardStatusDatabase cardStatus)
	{
		var query = "INSERT INTO card_status (card_id, status_id) " +
		            "VALUES ($1, $2)";

		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter() {Value = cardStatus.CardId},
			new NpgsqlParameter() {Value = cardStatus.StatusId}
		};

		return await ExecuteAsync(query, parameters);
	}

	public async Task<bool> DeleteCardStatusAsync(Guid cardId, Guid statusId)
	{
		var query = "DELETE FROM card_status WHERE card_id = $1 and status_id = $2";

		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter() {Value = cardId},
			new NpgsqlParameter() {Value = statusId}
		};

		return await ExecuteAsync(query, parameters);
	}

	public async Task<IEnumerable<CardTagsDatabase>> GetCardTagsAsync(Guid cardId)
	{
		var query = "SELECT * from card_tags where card_id = $1";

		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter() {Value = cardId}
		};

		return await GetListAsync<CardTagsDatabase>(query, parameters);
	}

	public async Task<CardTagsDatabase?> GetCardTagAsync(Guid cardId, Guid tagId)
	{
		var query = "SELECT * from card_tags where card_id = $1 and tag_id = $2";

		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter() {Value = cardId},
			new NpgsqlParameter() {Value = tagId}
		};

		return await GetAsync<CardTagsDatabase>(query, parameters);
	}

	public async Task<CardTagsDatabase?> GetCurrentCardTagAsync(Guid cardId)
	{
		var query = "SELECT * from card_tags where card_id = $1  limit 1";

		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter() {Value = cardId}
		};

		return await GetAsync<CardTagsDatabase>(query, parameters);
	}

	public async Task<bool> CreateCardTagAsync(CardTagsDatabase cardTag)
	{
		var query = "INSERT INTO card_tags (card_id, tag_id) " +
		            "VALUES ($1, $2)";

		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter() {Value = cardTag.CardId},
			new NpgsqlParameter() {Value = cardTag.TagId}
		};

		return await ExecuteAsync(query, parameters);
	}

	public async Task<bool> DeleteCardTagAsync(Guid cardId, Guid tagId)
	{
		var query = "DELETE FROM card_tags WHERE card_id = $1 and tag_id = $2";

		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter() {Value = cardId},
			new NpgsqlParameter() {Value = tagId}
		};

		return await ExecuteAsync(query, parameters);
	}

	public async Task<IEnumerable<CardUsersDatabase>> GetCardUsersAsync(Guid cardId)
	{
		var query = "SELECT * from card_users where card_id = $1";

		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter() {Value = cardId}
		};

		return await GetListAsync<CardUsersDatabase>(query, parameters);
	}

	public async Task<CardUsersDatabase?> GetCardUserAsync(Guid cardId, Guid userId)
	{
		var query = "SELECT * from card_users where card_id = $1 and user_id = $2";

		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter() {Value = cardId},
			new NpgsqlParameter() {Value = userId}
		};

		return await GetAsync<CardUsersDatabase>(query, parameters);
	}

	public async Task<bool> CreateCardUsersAsync(CardUsersDatabase cardUsersDatabase)
	{
		var query = "INSERT INTO card_users (card_id, user_id) " +
		            "VALUES ($1, $2)";

		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter() {Value = cardUsersDatabase.CardId},
			new NpgsqlParameter() {Value = cardUsersDatabase.UserId}
		};

		return await ExecuteAsync(query, parameters);
	}

	public async Task<bool> DeleteCardUsersAsync(Guid cardId, Guid userId)
	{
		var query = "DELETE FROM card_users WHERE card_id = $1 and user_id = $2";

		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter() {Value = cardId},
			new NpgsqlParameter() {Value = userId}
		};

		return await ExecuteAsync(query, parameters);
	}
}