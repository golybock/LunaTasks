using System.Text.Json;
using Luna.Models.Tasks.Blank.Card;
using Luna.Models.Tasks.Database.Card;
using Luna.Models.Tasks.Database.CardAttributes;
using Luna.Models.Tasks.Domain.Card;
using Luna.Models.Tasks.Domain.CardAttributes;
using Luna.Models.Tasks.View.Card;
using Luna.Models.Tasks.View.CardAttributes;
using Luna.Models.Users.View.Users;
using Luna.SharedDataAccess.Users.Services;
using Luna.Tasks.Repositories.Repositories.Card;
using Luna.Tasks.Services.Services.CardAttributes.Comment;
using Luna.Tasks.Services.Services.CardAttributes.Role;
using Luna.Tasks.Services.Services.CardAttributes.Status;
using Luna.Tasks.Services.Services.CardAttributes.Tag;
using Luna.Tasks.Services.Services.CardAttributes.Type;
using OfficeOpenXml;

namespace Luna.Tasks.Services.Services.Card;

public class CardService : ICardService
{
	private readonly ICardRepository _cardRepository;

	private readonly IUserService _userService;

	private readonly ITagService _tagService;
	private readonly IStatusService _statusService;
	private readonly IRoleService _roleService;
	private readonly ITypeService _typeService;
	private readonly ICommentService _commentService;

	public CardService
	(
		ICardRepository cardRepository, IUserService userService,
		ITagService tagService, IStatusService statusService,
		IRoleService roleService, ITypeService typeService,
		ICommentService commentService
	)
	{
		_cardRepository = cardRepository;
		_userService = userService;
		_tagService = tagService;
		_statusService = statusService;
		_roleService = roleService;
		_typeService = typeService;
		_commentService = commentService;
	}

	// get cards with filter
	public async Task<IEnumerable<CardView>> GetCardsAsync(Guid pageId, List<Guid> userIds, List<Guid> tagIds)
	{
		var cards = await _cardRepository.GetCardsAsync(pageId);

		var cardViews = await GetCardsByAsync(cards, userIds, tagIds);

		return cardViews;
	}

	public async Task<IEnumerable<CardView>> GetCardsAsync(Guid pageId, List<Guid> userIds)
	{
		var cards = await _cardRepository.GetCardsAsync(pageId);

		var cardViews = await GetCardsByAsync(cards, userIds);

		return cardViews;
	}

	// for trash or get witouh filter
	public async Task<IEnumerable<CardView>> GetCardsAsync(Guid pageId, bool deleted = false)
	{
		var cards = await _cardRepository.GetCardsAsync(pageId);

		var cardViews = await GetCardsAsync(cards);

		return cardViews;
	}

	public async Task<IEnumerable<CardView>> GetCardsAsync(IEnumerable<Guid> cardIds)
	{
		var enumerable = cardIds.ToList();

		List<CardView> cardViews = new List<CardView>(enumerable.Count());

		await Parallel.ForEachAsync(enumerable, async (cardId, ct) =>
		{
			var card = await GetCardAsync(cardId);

			cardViews.Add(card);
		});

		return cardViews;
	}

	private async Task<IEnumerable<CardView>> GetCardsByAsync(IEnumerable<CardDatabase> cardDatabases, List<Guid> userIds, List<Guid> tagIds)
	{
		var enumerable = cardDatabases.ToList();

		List<CardView> cardViews = new List<CardView>(enumerable.Count());

		await Parallel.ForEachAsync(enumerable, async (cardDatabase, ct) =>
		{
			var card = await GetCardViewBy(cardDatabase, userIds, tagIds);

			if (card != null) cardViews.Add(card);
		});

		return cardViews;
	}

	private async Task<IEnumerable<CardView>> GetCardsByAsync(IEnumerable<CardDatabase> cardDatabases, List<Guid> userIds)
	{
		var enumerable = cardDatabases.ToList();

		List<CardView> cardViews = new List<CardView>(enumerable.Count());

		await Parallel.ForEachAsync(enumerable, async (cardDatabase, ct) =>
		{
			var card = await GetCardViewBy(cardDatabase, userIds);

			if (card != null) cardViews.Add(card);
		});

		return cardViews;
	}

	private async Task<IEnumerable<CardView>> GetCardsAsync(IEnumerable<CardDatabase> cardDatabases)
	{
		var enumerable = cardDatabases.ToList();

		List<CardView> cardViews = new List<CardView>(enumerable.Count());

		await Parallel.ForEachAsync(enumerable, async (cardDatabase, ct) =>
		{
			var card = await GetCardView(cardDatabase);

			cardViews.Add(card);
		});

		return cardViews;
	}

	public async Task<CardView?> GetCardAsync(Guid id)
	{
		var cardDatabase = await _cardRepository.GetCardAsync(id);

		if (cardDatabase == null)
			return null;

		var card = await GetCardView(cardDatabase);

		return card;
	}

	public async Task<IEnumerable<CardDomain>> GetCardsDomainAsync(Guid pageId, Guid userId)
	{
		var cards = await _cardRepository.GetCardsAsync(pageId, userId);

		var cardDomains = await GetCardDomains(cards);

		return cardDomains;
	}

	// too slowly
	public async Task<IEnumerable<CardDomain>> GetCardsDomainAsync(Guid pageId)
	{
		var cards = await _cardRepository.GetCardsAsync(pageId);

		var cardDomains = await GetCardDomains(cards);

		return cardDomains;
	}

	public async Task<IEnumerable<CardDomain>> GetCardsPreviewAsync(Guid pageId)
	{
		var cards = await _cardRepository.GetCardsAsync(pageId);

		var cardDomains = await GetCardDomains(cards);

		return cardDomains;
	}

	public async Task<IEnumerable<CardDomain>> GetCardsDomainAsync(IEnumerable<Guid> cardIds)
	{
		var enumerable = cardIds.ToList();

		List<CardDomain> cardDomains = new List<CardDomain>(enumerable.Count());

		await Parallel.ForEachAsync(enumerable, async (cardId, ct) =>
		{
			var cardDatabase = await _cardRepository.GetCardAsync(cardId);

			var card = await GetCardDomain(cardDatabase);

			cardDomains.Add(card);
		});

		return cardDomains;
	}

	public async Task<CardDomain?> GetCardDomainAsync(Guid id)
	{
		var cardDatabase = await _cardRepository.GetCardAsync(id);

		if (cardDatabase == null)
			return null;

		var card = await GetCardDomain(cardDatabase);

		return card;
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

	public async Task<bool> DeleteCardAsync(Guid id, Guid userId)
	{
		return await _cardRepository.DeleteCardAsync(id);
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

		return await _cardRepository.CreateBlockedCardAsync(blockedCardDatabase);
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

	public async Task<IEnumerable<CardStatusDomain>> GetCardStatusesDomainAsync(Guid cardId)
	{
		var statuses = await _cardRepository.GetCardStatusesAsync(cardId);

		var statusIds = statuses.Select(s => s.StatusId);

		var statusDomains = await _statusService.GetStatusesDomainAsync(statusIds);

		var cardStatusDomains = new List<CardStatusDomain>();

		foreach (var status in statuses)
		{
			cardStatusDomains.Add(
				new CardStatusDomain(
					status,
					statusDomains.First(c => c.Id == status.StatusId)
				)
			);
		}

		return cardStatusDomains;
	}

	public async Task<StatusView?> GetCardStatusAsync(Guid cardId, Guid statusId)
	{
		var status = await _cardRepository.GetCardStatusAsync(cardId, statusId);

		if (status == null)
			return null;

		var statusView = await _statusService.GetStatusAsync(status.StatusId);

		return statusView;
	}

	public async Task<StatusView?> GetCurrentCardStatusAsync(Guid cardId)
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

	public async Task<IEnumerable<TagView>> GetCardTagsAsync(Guid cardId)
	{
		var cardTags = await _cardRepository.GetCardTagsAsync(cardId);

		var tagIds = cardTags.Select(t => t.TagId);

		var tags = await _tagService.GetTagsAsync(tagIds);

		return tags;
	}

	public async Task<IEnumerable<CardTagsDomain>> GetCardTagsDomainAsync(Guid cardId)
	{
		var cardTags = await _cardRepository.GetCardTagsAsync(cardId);

		var tagIds = cardTags.Select(t => t.TagId);

		var tags = await _tagService.GetTagsDomainAsync(tagIds);

		return cardTags.Select(t => new CardTagsDomain(t, tags.FirstOrDefault(domain => domain.Id == t.TagId)))
			.ToList();
	}

	// get CardTagsDomain by cardId and hash array
	private async Task<IEnumerable<CardTagsDomain>> GetCardTagsDomainByHashAsync(Guid cardId,
		HashSet<TagDomain> tagDomains)
	{
		var cardTags = await _cardRepository.GetCardTagsAsync(cardId);

		var cardTagsDomains = new List<CardTagsDomain>(cardTags.Count());

		foreach (var cardTag in cardTags)
		{
			var tagDomain = tagDomains.FirstOrDefault(item => item.Id == cardTag.TagId);

			if (tagDomain == null)
			{
				var tagDb = await _tagService.GetTagDomainAsync(cardTag.TagId);

				if (tagDb != null)
				{
					tagDomains.Add(tagDb);
					tagDomain = tagDb;
				}
			}

			var tag = new CardTagsDomain(cardTag, tagDomain);

			cardTagsDomains.Add(tag);
		}

		return cardTagsDomains;
	}

	public async Task<bool> CreateCardTagAsync(Guid cardId, Guid tagId, Guid userId)
	{
		var cardTagDatabase = new CardTagsDatabase()
		{
			CardId = cardId,
			TagId = tagId
		};

		return await _cardRepository.CreateCardTagAsync(cardTagDatabase);
	}

	private async Task<bool> UpdateCardTags(Guid cardId, IEnumerable<Guid> tagIds)
	{
		await DeleteCardTagsAsync(cardId);

		return await CreateCardTagsAsync(cardId, tagIds);
	}

	private async Task<bool> CreateCardTagsAsync(Guid cardId, IEnumerable<Guid> tagIds)
	{
		var tags = tagIds.Select(item => new CardTagsDatabase() {CardId = cardId, TagId = item});

		return await _cardRepository.CreateCardTagsAsync(tags);
	}

	private async Task<bool> DeleteCardTagsAsync(Guid cardId)
	{
		return await _cardRepository.DeleteCardTagsAsync(cardId);
	}

	public async Task<bool> DeleteCardTagAsync(Guid cardId, Guid tagId, Guid userId)
	{
		return await _cardRepository.DeleteCardTagAsync(cardId, tagId);
	}

	// todo вынести в отдельный сервайс
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
			var card = cards.ElementAt(i-2);

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
			sheet.Cells[i, 12].Value = JsonSerializer.Serialize(card.Statuses);
		}

		return await package.GetAsByteArrayAsync();
	}

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

	private async Task<bool> UpdateCardUsersAsync(Guid cardId, IEnumerable<Guid> userIds)
	{
		await DeleteCardUsersAsync(cardId);

		return await CreateCardUsersAsync(cardId, userIds);
	}

	public async Task<bool> CreateCardUsersAsync(Guid cardId, IEnumerable<Guid> userIds)
	{
		var users = userIds.Select(item => new CardUsersDatabase() {CardId = cardId, UserId = item});

		return await _cardRepository.CreateCardUsersAsync(users);
	}

	public async Task<bool> DeleteCardUsersAsync(Guid cardId, Guid userId)
	{
		var result = await _cardRepository.DeleteCardUserAsync(cardId, userId);

		return result;
	}

	public async Task<bool> DeleteCardUsersAsync(Guid cardId)
	{
		var result = await _cardRepository.DeleteCardUsersAsync(cardId);

		return result;
	}

	private async Task<CardView?> GetCardViewBy(CardDatabase cardDatabase, List<Guid> userIds, List<Guid> tagIds)
	{
		var cardDomain = new CardDomain(cardDatabase);

		var typeTask = _typeService.GetTypeAsync(cardDomain.CardTypeId);
		var commentsTask = _commentService.GetCommentsAsync(cardDomain.Id);
		var tagsTask = GetCardTagsAsync(cardDomain.Id);
		var statusesTask = GetCardStatusesAsync(cardDomain.Id);
		var usersTask = _cardRepository.GetCardUsersAsync(cardDomain.Id);

		var type = await typeTask;
		var comments = await commentsTask;
		var tags = await tagsTask;
		var statuses = await statusesTask;
		var users = await usersTask;

		var cardUserIds = users.Select(u => u.UserId);

		if (tagIds.Any())
		{
			if (!tags.ToList().Select(c => c.Id).Intersect(tagIds).Any())
				return null;
		}

		if (userIds.Any())
		{
			if (!cardUserIds.Intersect(userIds).Any())
				return null;
		}

		var usersViews = await _userService.GetUsersAsync();
		usersViews = usersViews.Where(c => userIds.Contains(c.Id)).ToList();

		return new CardView(cardDomain, type, comments, tags, usersViews, statuses);
	}

	private async Task<CardView?> GetCardViewBy(CardDatabase cardDatabase, List<Guid> userIds)
	{
		var cardDomain = new CardDomain(cardDatabase);

		var typeTask = _typeService.GetTypeAsync(cardDomain.CardTypeId);
		var commentsTask = _commentService.GetCommentsAsync(cardDomain.Id);
		var tagsTask = GetCardTagsAsync(cardDomain.Id);
		var statusesTask = GetCardStatusesAsync(cardDomain.Id);
		var usersTask = _cardRepository.GetCardUsersAsync(cardDomain.Id);

		var type = await typeTask;
		var comments = await commentsTask;
		var tags = await tagsTask;
		var statuses = await statusesTask;
		var users = await usersTask;

		var cardUserIds = users.Select(u => u.UserId);

		if (userIds.Any())
		{
			if (!cardUserIds.Intersect(userIds).Any())
				return null;
		}

		var usersViews = await _userService.GetUsersAsync();
		usersViews = usersViews.Where(c => userIds.Contains(c.Id)).ToList();

		return new CardView(cardDomain, type, comments, tags, usersViews, statuses);
	}

	private async Task<CardView> GetCardView(CardDatabase cardDatabase)
	{
		var cardDomain = new CardDomain(cardDatabase);

		var typeTask = _typeService.GetTypeAsync(cardDomain.CardTypeId);
		var commentsTask = _commentService.GetCommentsAsync(cardDomain.Id);
		var tagsTask = GetCardTagsAsync(cardDomain.Id);
		var statusesTask = GetCardStatusesAsync(cardDomain.Id);
		var usersTask = _cardRepository.GetCardUsersAsync(cardDomain.Id);

		var type = await typeTask;
		var comments = await commentsTask;
		var tags = await tagsTask;
		var statuses = await statusesTask;
		var users = await usersTask;

		var userIds = users.Select(u => u.UserId);

		var usersViews = await _userService.GetUsersAsync();
		usersViews = usersViews.Where(c => userIds.Contains(c.Id)).ToList();

		return new CardView(cardDomain, type, comments, tags, usersViews, statuses);
	}

	// its vety slow
	private async Task<CardDomain> GetCardDomain(CardDatabase cardDatabase)
	{
		var type = await _typeService.GetTypeDomainAsync(cardDatabase.CardTypeId);

		var comments = await _commentService.GetCommentsDomainAsync(cardDatabase.Id);

		var tags = await GetCardTagsDomainAsync(cardDatabase.Id);

		// todo refactor
		var statuses = await GetCardStatusesDomainAsync(cardDatabase.Id);

		var users = await _cardRepository.GetCardUsersAsync(cardDatabase.Id);

		var usersDomain = await _userService.GetUsersDomainAsync();

		// var userIds = users.Select(u => u.UserId);

		// todo refactor
		var cardUsers = users.Select(u =>
			new CardUsersDomain(u, usersDomain.First(user => user.Id == u.UserId)));

		return new CardDomain(cardDatabase, type, null, null, comments, tags, cardUsers, statuses);
	}

	private async Task<CardDomain> GetCardDomain(CardDatabase cardDatabase, HashSet<StatusDomain> statusDomains,
		HashSet<TagDomain> tagDomains, HashSet<TypeDomain> typeDomains)
	{
		// type
		var type = typeDomains.FirstOrDefault(c => c.Id == cardDatabase.CardTypeId);

		if (type == null)
		{
			var typeDb = await _typeService.GetTypeDomainAsync(cardDatabase.CardTypeId);
			if (typeDb != null)
			{
				typeDomains.Add(typeDb);
				type = typeDb;
			}
		}

		// unique
		var comments = await _commentService.GetCommentsDomainAsync(cardDatabase.Id);

		var tags = await GetCardTagsDomainByHashAsync(cardDatabase.Id, tagDomains);

		// todo refactor
		var statuses = await GetCardStatusesDomainAsync(cardDatabase.Id);

		// unique
		// maybe get for all cards only time
		var users = await _cardRepository.GetCardUsersAsync(cardDatabase.Id);

		// var userIds = users.Select(u => u.UserId);

		var usersDomain = await _userService.GetUsersDomainAsync();

		// todo refactor
		var cardUsers = users.Select(u =>
			new CardUsersDomain(u, usersDomain.First(user => user.Id == u.UserId)));

		return new CardDomain(cardDatabase, type, null, null, comments, tags, cardUsers, statuses);
	}

	private async Task<IEnumerable<CardDomain>> GetCardDomains(IEnumerable<CardDatabase> cardDatabases)
	{
		var cardDomains = new List<CardDomain>(cardDatabases.Count());

		// подобие кэша
		var statuses = new HashSet<StatusDomain>();
		var tags = new HashSet<TagDomain>();
		var type = new HashSet<TypeDomain>();

		foreach (var cardDatabase in cardDatabases)
		{
			// refactor this shit
			var cardDomain = await GetCardDomain(cardDatabase, statuses, tags, type);
			cardDomains.Add(cardDomain);
		}

		return cardDomains;
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

	private BlockedCardView ToBlockedCardView(BlockedCardDatabase blockedCardDatabase)
	{
		var blockedCardDomain = new BlockedCardDomain(blockedCardDatabase);
		return new BlockedCardView(blockedCardDomain);
	}

	private IEnumerable<BlockedCardView> ToBlockedCardView(IEnumerable<BlockedCardDatabase> blockedCardDatabases)
	{
		return blockedCardDatabases.Select(ToBlockedCardView).ToList();
	}

	private TagView ToTagView(TagDatabase tag)
	{
		var tagDomain = new TagDomain(tag);
		return new TagView(tagDomain);
	}

	private IEnumerable<TagView> ToTagView(IEnumerable<TagDatabase> tags)
	{
		return tags.Select(ToTagView).ToList();
	}

	private async Task<UserView?> GetUserAsync(Guid userId)
	{
		var user = await _userService.GetUserAsync(userId);

		return user;
	}

	// todo fix this
	private async Task<IEnumerable<UserView>> GetUsersAsync(IEnumerable<Guid> ids)
	{
		var users = await _userService.GetUsersAsync();

		return users.Where(c => ids.Contains(c.Id)).ToList();
	}
}