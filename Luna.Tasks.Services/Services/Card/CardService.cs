using Luna.Models.Tasks.Blank.Card;
using Luna.Models.Tasks.Database.Card;
using Luna.Models.Tasks.Database.CardAttributes;
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
		throw new NotImplementedException();
	}

	public async Task<IEnumerable<CardView>> GetCardsAsync(Guid pageId)
	{
		throw new NotImplementedException();
	}

	public async Task<IEnumerable<CardView>> GetCardsAsync(IEnumerable<Guid> cardIds)
	{
		throw new NotImplementedException();
	}

	public async Task<CardView?> GetCardAsync(Guid id)
	{
		throw new NotImplementedException();
	}

	public async Task<bool> CreateCardAsync(CardBlank card, Guid userId)
	{
		throw new NotImplementedException();
	}

	public async Task<bool> UpdateCardAsync(Guid id, CardBlank card, Guid userId)
	{
		throw new NotImplementedException();
	}

	public async Task<bool> DeleteCardAsync(Guid id, Guid userId)
	{
		return await _cardRepository.DeleteCardAsync(id);
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
		return await _cardRepository.DeleteBlockedCardAsync(cardId);
	}

	public async Task<IEnumerable<CardStatusView>> GetCardStatusesAsync(Guid cardId)
	{
		throw new NotImplementedException();
	}

	public async Task<CardStatusView?> GetCardStatusAsync(Guid cardId, Guid statusId)
	{
		throw new NotImplementedException();
	}

	public async Task<CardStatusView?> GetCurrentCardStatusAsync(Guid cardId)
	{
		throw new NotImplementedException();
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