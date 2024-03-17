﻿using Luna.Models.Tasks.Blank.Card;
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

	public async Task<IEnumerable<CardView>> GetCardsAsync(Guid pageId, Guid userId)
	{
		var cards = await _cardRepository.GetCardsAsync(pageId, userId);

		var cardViews = await GetCardsAsync(cards);

		return cardViews;
	}

	public async Task<IEnumerable<CardView>> GetCardsAsync(Guid pageId)
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

	public async Task<bool> CreateCardAsync(CardBlank card, Guid userId)
	{
		var cardDatabase = ToCardDatabase(card, userId);

		var result = await _cardRepository.CreateCardAsync(cardDatabase);

		return result;
	}

	public async Task<bool> UpdateCardAsync(Guid id, CardBlank card, Guid userId)
	{
		var cardDatabase = ToCardDatabase(card, userId);

		var result = await _cardRepository.UpdateCardAsync(id, cardDatabase);

		return result;
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

	public async Task<bool> CreateCardStatusAsync(Guid cardId, Guid statusId, Guid userId)
	{
		var cardStatusDatabase = new CardStatusDatabase()
		{
			CardId = cardId,
			StatusId = statusId,
			SetTimestamp = DateTime.UtcNow
		};

		return await _cardRepository.CreateCardStatusAsync(cardStatusDatabase);
	}

	public async Task<bool> DeleteCardStatusAsync(Guid cardId, Guid statusId, Guid userId)
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

	public async Task<bool> CreateCardTagAsync(Guid cardId, Guid tagId, Guid userId)
	{
		var cardTagDatabase = new CardTagsDatabase()
		{
			CardId = cardId,
			TagId = tagId
		};

		return await _cardRepository.CreateCardTagAsync(cardTagDatabase);
	}

	public async Task<bool> DeleteCardTagAsync(Guid cardId, Guid tagId, Guid userId)
	{
		return await _cardRepository.DeleteCardTagAsync(cardId, tagId);
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

		return await _cardRepository.CreateCardUsersAsync(cardUsersDatabase);
	}

	public async Task<bool> DeleteCardUsersAsync(Guid cardId, Guid userId)
	{
		var result = await _cardRepository.DeleteCardUsersAsync(cardId, userId);

		return result;
	}

	private async Task<CardView> GetCardView(CardDatabase cardDatabase)
	{
		var cardDomain = new CardDomain(cardDatabase);

		var type = await _typeService.GetTypeAsync(cardDomain.CardTypeId);

		var comments = await _commentService.GetCommentsAsync(cardDomain.Id);

		var tags = await GetCardTagsAsync(cardDomain.Id);

		var statuses = await GetCardStatusesAsync(cardDomain.Id);

		var users = await _cardRepository.GetCardUsersAsync(cardDomain.Id);

		var userIds = users.Select(u => u.UserId);

		return new CardView(cardDomain, type, comments, tags, userIds, statuses);
	}

	private CardDatabase ToCardDatabase(CardBlank cardBlank, Guid userId)
	{
		return new CardDatabase()
		{
			Id = Guid.NewGuid(),
			Deadline = cardBlank.Deadline,
			Content = cardBlank.Content,
			Header = cardBlank.Header,
			CreatedTimestamp = DateTime.UtcNow,
			PageId = cardBlank.PageId,
			PreviousCardId = cardBlank.PreviousCardId,
			Description = cardBlank.Description,
			Deleted = false,
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