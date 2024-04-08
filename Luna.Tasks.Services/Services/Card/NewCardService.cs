using System.Text.Json;
using Luna.Models.Tasks.Blank.Card;
using Luna.Models.Tasks.Database.Card;
using Luna.Models.Tasks.Domain.Card;
using Luna.Models.Tasks.Domain.CardAttributes;
using Luna.Models.Tasks.View.Card;
using Luna.Models.Tasks.View.CardAttributes;
using Luna.Models.Users.View.Users;
using Luna.SharedDataAccess.Users.Services;
using Luna.Tasks.Repositories.Repositories.Card;
using Luna.Tasks.Services.Services.CardAttributes.Comment;
using Luna.Tasks.Services.Services.CardAttributes.Status;
using Luna.Tasks.Services.Services.CardAttributes.Tag;
using Luna.Tasks.Services.Services.CardAttributes.Type;
using OfficeOpenXml;

namespace Luna.Tasks.Services.Services.Card;

public class NewCardService : ICardService
{
	private readonly ICardRepository _cardRepository;

	private readonly IUserService _userService;

	private readonly ITagService _tagService;
	private readonly IStatusService _statusService;
	// private readonly IRoleService _roleService;
	private readonly ITypeService _typeService;
	private readonly ICommentService _commentService;

	public NewCardService
	(
		ICardRepository cardRepository,
		IUserService userService,
		ITagService tagService,
		IStatusService statusService,
		// IRoleService roleService,
		ITypeService typeService,
		ICommentService commentService
	)
	{
		_cardRepository = cardRepository;
		_userService = userService;
		_tagService = tagService;
		_statusService = statusService;
		// _roleService = roleService;
		_typeService = typeService;
		_commentService = commentService;
	}

	public async Task<IEnumerable<CardView>> GetCardsByUsersAsync(Guid pageId, List<Guid> userIds)
	{
		throw new NotImplementedException();
	}

	public async Task<IEnumerable<CardView>> GetCardsByTagsAsync(Guid pageId, List<Guid> tagIds)
	{
		throw new NotImplementedException();
	}

	public async Task<CardView?> GetCardAsync(Guid id)
	{
		throw new NotImplementedException();
	}

	// todo добавить единовременное получение всех данных из бд, во внутренних методах только конвертировать модели
	public async Task<IEnumerable<CardView>> GetCardsAsync(Guid pageId, bool deleted = false)
	{
		var cardDatabases = await _cardRepository.GetCardsAsync(pageId, deleted);

		var cardIds = cardDatabases.Select(card => card.Id).ToList();

		var cardsTags = await _cardRepository.GetCardTagsAsync(cardIds);
		var cardsStatuses = await _cardRepository.GetCardStatusesAsync(cardIds);
		var cardsUsers = await _cardRepository.GetCardsUsersAsync(cardIds);
		var cardsComments = await _commentService.GetCommentsDomainAsync(cardIds);

		List<CardView> cardViews = new List<CardView>(cardIds.Count());

		await Parallel.ForEachAsync(cardDatabases, async (cardDatabase, ct) =>
		{
			var tags = cardsTags.Where(c => c.CardId == cardDatabase.Id);
			var statuses = cardsStatuses.Where(c => c.CardId == cardDatabase.Id);
			var users = cardsUsers.Where(c => c.CardId == cardDatabase.Id);
			var comments = cardsComments.Where(c => c.CardId == cardDatabase.Id);

			var card = await GetCardView(cardDatabase, tags, statuses, users, comments);

			cardViews.Add(card);
		});

		return cardViews;
	}

	public async Task<IEnumerable<CardView>> GetCardsAsync(IEnumerable<Guid> cardIds)
	{
		var cardDatabases = await _cardRepository.GetCardsAsync(cardIds);

		var cardsTags = await _cardRepository.GetCardTagsAsync(cardIds);
		var cardsStatuses = await _cardRepository.GetCardStatusesAsync(cardIds);
		var cardsUsers = await _cardRepository.GetCardsUsersAsync(cardIds);
		var cardsComments = await _commentService.GetCommentsDomainAsync(cardIds);

		List<CardView> cardViews = new List<CardView>(cardIds.Count());

		await Parallel.ForEachAsync(cardDatabases, async (cardDatabase, ct) =>
		{
			var tags = cardsTags.Where(c => c.CardId == cardDatabase.Id);
			var statuses = cardsStatuses.Where(c => c.CardId == cardDatabase.Id);
			var users = cardsUsers.Where(c => c.CardId == cardDatabase.Id);
			var comments = cardsComments.Where(c => c.CardId == cardDatabase.Id);

			var card = await GetCardView(cardDatabase, tags, statuses, users, comments);

			cardViews.Add(card);
		});

		return cardViews;
	}


	public async Task<bool> CreateCardAsync(CardBlank card, Guid userId)
	{
		throw new NotImplementedException();
	}

	public async Task<bool> UpdateCardAsync(Guid id, CardBlank card, Guid userId)
	{
		throw new NotImplementedException();
	}

	public async Task<bool> ToTrashCardAsync(Guid id, Guid userId)
	{
		throw new NotImplementedException();
	}

	public async Task<bool> ToTrashCardsAsync(IEnumerable<Guid> id, Guid userId)
	{
		throw new NotImplementedException();
	}

	public async Task<bool> DeleteCardAsync(Guid id, Guid userId)
	{
		throw new NotImplementedException();
	}

	private async Task<CardView> GetCardView
	(
		CardDatabase cardDatabase,
		IEnumerable<CardTagsDatabase> cardTags,
		IEnumerable<CardStatusDatabase> cardStatuses,
		IEnumerable<CardUsersDatabase> cardUsers,
		IEnumerable<CommentDomain> cardComments
	)
	{
		var cardDomain = new CardDomain(cardDatabase);

		var tagIds = cardTags.Select(c => c.TagId);
		var userIds = cardUsers.Select(u => u.UserId);
		var statusIds = cardStatuses.Select(c => c.StatusId);

		var tagsTask = GetagsAsync(tagIds);
		var statusesTask = GetStatusesAsync(statusIds);
		var typeTask = _typeService.GetTypeAsync(cardDomain.CardTypeId);

		var type = await typeTask;
		var tags = await tagsTask;
		var comments = cardComments.Select(c => new CommentView(c));
		var statuses = await statusesTask;

		var usersViews = await _userService.GetUsersAsync();
		usersViews = usersViews.Where(c => userIds.Contains(c.Id)).ToList();

		return new CardView(cardDomain, type, comments, tags, usersViews, statuses.LastOrDefault());
	}

	public async Task<BlockedCardView?> GetBlockedCardAsync(Guid cardId)
	{
		throw new NotImplementedException();
	}

	public async Task<IEnumerable<BlockedCardView>> GetBlockedCardsAsync(IEnumerable<Guid> cardIds)
	{
		throw new NotImplementedException();
	}

	public async Task<bool> CreateBlockedCardAsync(BlockedCardBlank blockedCard, Guid userId)
	{
		throw new NotImplementedException();
	}

	public async Task<bool> UpdateBlockedCardAsync(Guid cardId, BlockedCardBlank blockedCard, Guid userId)
	{
		throw new NotImplementedException();
	}

	public async Task<bool> DeleteBlockedCardAsync(Guid cardId, Guid userId)
	{
		throw new NotImplementedException();
	}

	public async Task<IEnumerable<StatusView>> GetCardStatusesAsync(Guid cardId)
	{
		var statuses = await _cardRepository.GetCardStatusesAsync(cardId);

		var statusIds = statuses.Select(s => s.StatusId);

		var statusViews = await _statusService.GetStatusesAsync(statusIds);

		return statusViews;
	}

	public async Task<StatusView?> GetLastCardStatusAsync(Guid cardId)
	{
		var cardStatus = await _cardRepository.GetCurrentCardStatusAsync(cardId);

		if (cardStatus == null)
			return null;

		var statusView = await _statusService.GetStatusAsync(cardStatus.StatusId);

		return statusView;
	}

	public async Task<bool> CreateCardStatusAsync(Guid cardId, Guid statusId)
	{
		var cardStatusDatabase = new CardStatusDatabase()
		{
			CardId = cardId,
			StatusId = statusId,
			SetTimestamp = DateTime.UtcNow
		};

		return await _cardRepository.CreateCardStatusAsync(cardStatusDatabase);
	}

	public async Task<bool> DeleteCardStatusAsync(Guid cardId, Guid statusId)
	{
		return await _cardRepository.DeleteCardStatusAsync(cardId, statusId);
	}

	public async Task<bool> DeleteCardStatusesAsync(Guid cardId)
	{
		return await _cardRepository.DeleteCardStatusAsync(cardId);
	}

	#region card tags

	public async Task<IEnumerable<TagView>> GetCardTagsAsync(Guid cardId)
	{
		var cardTags = await _cardRepository.GetCardTagsAsync(cardId);

		var tagIds = cardTags.Select(t => t.TagId);

		var tags = await _tagService.GetTagsAsync(tagIds);

		return tags;
	}

	// предпочтительный вариант для сборка итоговой модели карточек
	public async Task<IEnumerable<TagView>> GetCardsTagsAsync(IEnumerable<Guid> cardIds)
	{
		var cardTags = await _cardRepository.GetCardTagsAsync(cardIds);

		var tagIds = cardTags.Select(t => t.TagId);

		var tags = await _tagService.GetTagsAsync(tagIds);

		return tags;
	}

	public async Task<bool> CreateCardTagAsync(Guid cardId, Guid tagId)
	{
		var cardTagDatabase = new CardTagsDatabase()
		{
			CardId = cardId,
			TagId = tagId
		};

		return await _cardRepository.CreateCardTagAsync(cardTagDatabase);
	}

	public async Task<bool> CreateCardTagsAsync(Guid cardId, IEnumerable<Guid> tagIds)
	{
		CardTagsDatabase ToCardTagDatabase(Guid item) => new CardTagsDatabase() {CardId = cardId, TagId = item};

		var tags = tagIds.Select(ToCardTagDatabase);

		return await _cardRepository.CreateCardTagsAsync(tags);
	}

	public async Task<bool> UpdateCardTags(Guid cardId, IEnumerable<Guid> tagIds)
	{
		var deleteResult = await DeleteCardTagsAsync(cardId);

		var createResult = await CreateCardTagsAsync(cardId, tagIds);

		return createResult && deleteResult;
	}

	public async Task<bool> DeleteCardTagAsync(Guid cardId, Guid tagId)
	{
		return await _cardRepository.DeleteCardTagAsync(cardId, tagId);
	}

	public async Task<bool> DeleteCardTagsAsync(Guid cardId)
	{
		return await _cardRepository.DeleteCardTagsAsync(cardId);
	}

	#endregion

	#region card users

	public async Task<IEnumerable<UserView>> GetCardUsersAsync(Guid cardId)
	{
		var users = await _cardRepository.GetCardUsersAsync(cardId);

		var userIds = users.Select(u => u.UserId);

		var usersView = await GetUsersAsync(userIds);

		return usersView;
	}

	public async Task<bool> CreateCardUsersAsync(Guid cardId, Guid userId)
	{
		var cardUsersDatabase = new CardUsersDatabase()
		{
			CardId = cardId,
			UserId = userId
		};

		return await _cardRepository.CreateCardUserAsync(cardUsersDatabase);
	}

	public async Task<bool> CreateCardUsersAsync(Guid cardId, IEnumerable<Guid> userIds)
	{
		CardUsersDatabase ToCardUsersDatabase(Guid userId) =>
			new CardUsersDatabase() {CardId = cardId, UserId = userId};

		var users = userIds.Select(ToCardUsersDatabase);

		return await _cardRepository.CreateCardUsersAsync(users);
	}

	public async Task<bool> UpdateCardUsersAsync(Guid cardId, IEnumerable<Guid> userIds)
	{
		var deleteResult = await DeleteCardUsersAsync(cardId);

		var createResult = await CreateCardUsersAsync(cardId, userIds);

		return createResult && deleteResult;
	}

	public async Task<bool> DeleteCardUsersAsync(Guid cardId, Guid userId)
	{
		return await _cardRepository.DeleteCardUserAsync(cardId, userId);
	}

	public async Task<bool> DeleteCardUsersAsync(Guid cardId)
	{
		return await _cardRepository.DeleteCardUsersAsync(cardId);
	}

	#endregion

	#region export

	public async Task<byte[]> GetCardsXlsx(Guid pageId)
	{
		var cards = await GetCardsAsync(pageId);

		var package = new ExcelPackage();

		var sheet = package.Workbook.Worksheets.Add("Tasks");

		// header
		sheet.Cells["A1"].Value = "Id";
		sheet.Cells["B1"].Value = "Header";
		sheet.Cells["C1"].Value = "Content";
		sheet.Cells["D1"].Value = "Description";
		sheet.Cells["E1"].Value = "Type";
		sheet.Cells["F1"].Value = "CreatedBy";
		sheet.Cells["G1"].Value = "Deadline";
		sheet.Cells["H1"].Value = "PreviousCardId";
		sheet.Cells["I1"].Value = "Comments";
		sheet.Cells["J1"].Value = "CardTags";
		sheet.Cells["K1"].Value = "Users";
		sheet.Cells["L1"].Value = "Statuses";

		sheet.Cells["A1:L1"].Style.Font.Bold = true;

		Console.WriteLine(cards.Count());

		for (int i = 2; i < cards.Count() + 2; i++)
		{
			var card = cards.ElementAt(i - 2);

			sheet.Cells[i, 1].Value = card.Id;
			sheet.Cells[i, 2].Value = card.Header;
			sheet.Cells[i, 3].Value = card.Content;
			sheet.Cells[i, 4].Value = card.Description;
			sheet.Cells[i, 5].Value = card.CardType.Name;
			sheet.Cells[i, 6].Value = card.CreatedUserId;
			sheet.Cells[i, 7].Value = card.Deadline;
			sheet.Cells[i, 8].Value = card.PreviousCard?.Id;
			sheet.Cells[i, 9].Value = JsonSerializer.Serialize(card.Comments);
			sheet.Cells[i, 10].Value = JsonSerializer.Serialize(card.CardTags);
			sheet.Cells[i, 11].Value = JsonSerializer.Serialize(card.Users);
			sheet.Cells[i, 12].Value = JsonSerializer.Serialize(card.Status);
		}

		return await package.GetAsByteArrayAsync();
	}

	#endregion

	// todo fix this
	private async Task<IEnumerable<UserView>> GetUsersAsync(IEnumerable<Guid> ids)
	{
		var users = await _userService.GetUsersAsync();

		return users.Where(c => ids.Contains(c.Id)).ToList();
	}

	private async Task<IEnumerable<TagView>> GetagsAsync(IEnumerable<Guid> tagIds)
	{
		var tags = await _tagService.GetTagsAsync(tagIds);

		return tags;
	}

	private async Task<IEnumerable<StatusView>> GetStatusesAsync(IEnumerable<Guid> statusIds)
	{
		var statusViews = await _statusService.GetStatusesAsync(statusIds);

		return statusViews;
	}
}