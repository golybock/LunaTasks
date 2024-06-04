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
using Xceed.Words.NET;

namespace Luna.Tasks.Services.Services.Card;

public class CardService : ICardService
{
	private readonly ICardRepository _cardRepository;

	private readonly IUserService _userService;

	private readonly ITagService _tagService;
	private readonly IStatusService _statusService;
	// private readonly IRoleService _roleService;
	private readonly ITypeService _typeService;
	private readonly ICommentService _commentService;

	public CardService
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
		var cards = await GetCardsAsync(pageId);

		var filteredCards = cards.Where(c => c.Users.Select(c => c.Id).Intersect(userIds).Any());

		return filteredCards;
	}

	public async Task<IEnumerable<CardView>> GetCardsByTagsAsync(Guid pageId, List<Guid> tagIds)
	{
		var cards = await GetCardsAsync(pageId);

		var filteredCards = cards.Where(c => c.CardTags.Select(c => c.Id).Intersect(tagIds).Any());

		return filteredCards;
	}

	public async Task<CardView?> GetCardAsync(Guid id)
	{
		var cardDatabase = await _cardRepository.GetCardAsync(id);

		if (cardDatabase == null)
			return null;

		var tags = await _cardRepository.GetCardTagsAsync(id);
		var statuses = await _cardRepository.GetCardStatusesAsync(id);
		var users = await _cardRepository.GetCardUsersAsync(id);
		var cardsComments = await _commentService.GetCommentsDomainAsync(id);

		var tagIds = tags.Select(c => c.TagId);
		var statusIds = statuses.Select(c => c.StatusId);
		var userIds = users.Select(u => u.UserId);

		var card = await ConvertToCardView(cardDatabase,tagIds, statusIds, userIds, cardsComments);

		return card;
	}

	// todo добавить единовременное получение всех данных из бд, во внутренних методах только конвертировать модели
	public async Task<IEnumerable<CardView>> GetCardsAsync(Guid pageId, bool deleted = false)
	{
		var cardDatabases = await _cardRepository.GetCardsAsync(pageId, deleted);

		var cardIds = cardDatabases.Select(c => c.Id);

		return await GetCardsAsync(cardIds, cardDatabases);
	}

	public async Task<IEnumerable<CardView>> GetCardsByWorkspaceAsync(Guid workspaceId)
	{
		var cardDatabases = await _cardRepository.GetCardsByWorkspaceAsync(workspaceId);

		var cardIds = cardDatabases.Select(c => c.Id);

		return await GetCardsAsync(cardIds, cardDatabases);
	}

	public async Task<IEnumerable<CardView>> GetCardsAsync(IEnumerable<Guid> cardIds)
	{
		var cardDatabases = await _cardRepository.GetCardsAsync(cardIds);

		return await GetCardsAsync(cardIds, cardDatabases);
	}

	private async Task<IEnumerable<CardView>> GetCardsAsync(IEnumerable<Guid> cardIds, IEnumerable<CardDatabase> cardDatabases)
	{
		// промежуточные таблицы с ids
		var cardsTags = await _cardRepository.GetCardTagsAsync(cardIds);
		var cardsStatuses = await _cardRepository.GetCardStatusesAsync(cardIds);
		var cardsUsers = await _cardRepository.GetCardsUsersAsync(cardIds);


		var cardsComments = await _commentService.GetCommentsDomainAsync(cardIds);

		// id всех типов и загрузка их моделей из бд
		var typeIds = cardDatabases.Select(card => card.CardTypeId).Distinct();
		var types = await _typeService.GetTypesAsync(typeIds);

		// id всех тегов и загрузка их моделей из бд
		var tagIds = cardsTags.Select(c => c.TagId);
		var tagsView = await GeTagsAsync(tagIds);

		// id всех статустов и загрузка их моделей из бд
		var statusIds = cardsStatuses.Select(c => c.StatusId);
		var statusesView = await GetStatusesAsync(statusIds);

		// id всех пользователей и загрузка их моделей из бд
		var userIds = cardsUsers.Select(u => u.UserId);
		var usersViews = await GetUsersAsync(userIds);

		// var commentIds = cardsComments.Select(comment => comment.Id);
		var commentsView = await _commentService.GetCommentsAsync(cardIds);

		List<CardView> cardViews = new List<CardView>(cardIds.Count());

		await Parallel.ForEachAsync(cardDatabases, async (cardDatabase, ct) =>
		{
			var tags = cardsTags.Where(c => c.CardId == cardDatabase.Id);
			var statuses = cardsStatuses.Where(c => c.CardId == cardDatabase.Id);
			var users = cardsUsers.Where(c => c.CardId == cardDatabase.Id);
			var comments = cardsComments.Where(c => c.CardId == cardDatabase.Id);

			// rename and optimaze
			var t = types.FirstOrDefault(c => c.Id == cardDatabase.CardTypeId);
			var tgs = tagsView.Where(c => tags.Select(b => b.TagId).Contains(c.Id));
			var s = statusesView.LastOrDefault(c => statuses.Select(b => b.StatusId).Contains(c.Id));
			var u = usersViews.Where(c => users.Select(b => b.UserId).Contains(c.Id));
			var c = commentsView.Where(c => comments.Select(b => b.Id).Contains(c.Id));

			var card = await ConvertToCardView(cardDatabase, t, tgs, s, c, u);

			cardViews.Add(card);
		});

		return cardViews;
	}


	public async Task<bool> CreateCardAsync(CardBlank card, Guid userId)
	{
		var cardDatabase = ToCardDatabase(card, userId);

		var result = await _cardRepository.CreateCardAsync(cardDatabase);

		await UpdateCardTags(cardDatabase.Id, card.TagIds);

		await UpdateCardUsersAsync(cardDatabase.Id, card.UserIds);

		await CreateCardStatusAsync(cardDatabase.Id, card.StatusId);

		return result;
	}

	public async Task<bool> UpdateCardAsync(Guid id, CardBlank card, Guid userId)
	{
		var cardDatabase = ToCardDatabase(card, userId);

		var result = await _cardRepository.UpdateCardAsync(id, cardDatabase);

		await UpdateCardTags(id, card.TagIds);

		await UpdateCardUsersAsync(id, card.UserIds);

		await CreateCardStatusAsync(id, card.StatusId);

		return result;
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
		return await _cardRepository.DeleteCardAsync(id);
	}

	private async Task<CardView> ConvertToCardView
	(
		CardDatabase cardDatabase,
		IEnumerable<Guid> tagIds,
		IEnumerable<Guid> statusIds,
		IEnumerable<Guid> userIds,
		IEnumerable<CommentDomain> cardComments
	)
	{
		var cardDomain = new CardDomain(cardDatabase);
		var tags = await GeTagsAsync(tagIds);
		var statuses = await GetStatusesAsync(statusIds);
		var type = await _typeService.GetTypeAsync(cardDomain.CardTypeId);
		var comments = cardComments.Select(c => new CommentView(c));

		var usersViews = await GetUsersAsync(userIds);

		return new CardView(cardDomain, type, comments, tags, usersViews, statuses.LastOrDefault());
	}

	private async Task<CardView> ConvertToCardView
	(
		CardDatabase cardDatabase,
		TypeView type,
		IEnumerable<TagView> tags,
		StatusView status,
		IEnumerable<CommentView> cardComments,
		IEnumerable<UserView> users
		)
	{
		var cardDomain = new CardDomain(cardDatabase);

		return new CardView(cardDomain, type, cardComments, tags, users, status);
	}

	public async Task<BlockedCardView?> GetBlockedCardAsync(Guid cardId)
	{
		var blockedCard = await _cardRepository.GetBlockedCardAsync(cardId);

		if (blockedCard == null)
			return null;

		return ToBlockedCardView(blockedCard);
	}

	public async Task<IEnumerable<BlockedCardView>> GetBlockedCardsAsync(IEnumerable<Guid> cardIds)
	{
		var blockedCard = await _cardRepository.GetBlockedCardsAsync(cardIds);

		return ToBlockedCardView(blockedCard);
	}

	public async Task<bool> CreateBlockedCardAsync(BlockedCardBlank blockedCard, Guid userId)
	{
		var blockedCardDatabase = new BlockedCardDatabase()
		{
			BlockedUserId = userId,
			CardId = blockedCard.CardId,
			Comment = blockedCard.Comment,
			EndBlockTimestamp = blockedCard.EndBlockTimestamp,
			StartBlockTimestamp = blockedCard.StartBlockTimestamp
		};

		return await _cardRepository.CreateBlockedCardAsync(blockedCardDatabase);;
	}

	public async Task<bool> UpdateBlockedCardAsync(Guid cardId, BlockedCardBlank blockedCard, Guid userId)
	{
		var blockedCardDatabase = new BlockedCardDatabase()
		{
			Comment = blockedCard.Comment,
			EndBlockTimestamp = blockedCard.EndBlockTimestamp,
			StartBlockTimestamp = blockedCard.StartBlockTimestamp
		};

		return await _cardRepository.UpdateBlockedCardAsync(cardId, blockedCardDatabase);
	}

	public async Task<bool> DeleteBlockedCardAsync(Guid cardId, Guid userId)
	{
		return await _cardRepository.DeleteBlockedCardAsync(cardId);
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

	public async Task<byte[]> GetCardDocument(Guid cardId)
	{
		var card = await GetCardAsync(cardId);

		var path = $"Files/{Guid.NewGuid()}.docx";

		var doc = DocX.Create(path);

		var paragraph = doc.InsertParagraph();
		paragraph.AppendLine($"Id: {card.Id}");
		paragraph.AppendLine($"Description: {card.Description}");
		paragraph.AppendLine($"Content: {card.Content}");
		paragraph.AppendLine($"Status: {card.Status.Name}");

		doc.Save();

		return await File.ReadAllBytesAsync(path);
	}

	#endregion

	// todo fix this
	private async Task<IEnumerable<UserView>> GetUsersAsync(IEnumerable<Guid> ids)
	{
		var users = await _userService.GetUsersAsync();

		return users.Where(c => ids.Contains(c.Id)).ToList();
	}

	private async Task<IEnumerable<TagView>> GeTagsAsync(IEnumerable<Guid> tagIds)
	{
		var tags = await _tagService.GetTagsAsync(tagIds);

		return tags;
	}

	private async Task<IEnumerable<StatusView>> GetStatusesAsync(IEnumerable<Guid> statusIds)
	{
		var statusViews = await _statusService.GetStatusesAsync(statusIds);

		return statusViews;
	}

	#region models convert

	private BlockedCardView ToBlockedCardView(BlockedCardDatabase blockedCardDatabase)
	{
		var blockedCardDomain = new BlockedCardDomain(blockedCardDatabase);
		return new BlockedCardView(blockedCardDomain);
	}

	private IEnumerable<BlockedCardView> ToBlockedCardView(IEnumerable<BlockedCardDatabase> blockedCardDatabases)
	{
		return blockedCardDatabases.Select(ToBlockedCardView).ToList();
	}

	private CardDatabase ToCardDatabase(CardBlank cardBlank, Guid userId)
	{
		return new CardDatabase()
		{
			Id = Guid.NewGuid(),
			Content = cardBlank.Content,
			Header = cardBlank.Header,
			CreatedTimestamp = DateTime.UtcNow,
			PageId = cardBlank.PageId,
			PreviousCardId = cardBlank.PreviousCardId,
			Description = cardBlank.Description,
			Deleted = false,
			Deadline = cardBlank.Deadline,
			CardTypeId = cardBlank.CardTypeId,
			CreatedUserId = userId
		};
	}

	#endregion
}