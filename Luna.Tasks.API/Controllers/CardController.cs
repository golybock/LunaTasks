using Luna.Models.Tasks.Blank.Card;
using Luna.Models.Tasks.View.Card;
using Luna.Tasks.Services.Services.Card;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ControllerBase = Luna.Tools.Web.ControllerBase;

namespace Luna.Tasks.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CardController : ControllerBase
{
	private readonly ICardService _cardService;

	public CardController(ICardService cardService)
	{
		_cardService = cardService;
	}

	[HttpGet("[action]")]
	public async Task<IEnumerable<CardView>> GetCardsAsync(Guid pageId, [FromQuery] List<Guid> userIds)
	{
		if (!userIds.Any())
			return await _cardService.GetCardsAsync(pageId);

		return await _cardService.GetCardsAsync(pageId, userIds);
	}

	[HttpGet("[action]")]
	public async Task<CardView?> GetCardAsync(Guid id)
	{
		return await _cardService.GetCardAsync(id);
	}

	[HttpPost("[action]")]
	public async Task<IActionResult> CreateCardAsync(CardBlank card)
	{
		var result = await _cardService.CreateCardAsync(card, UserId);

		return result ? Ok() : BadRequest();
	}

	[HttpGet("[action]")]
	public async Task<IActionResult> GetCardsXlsx(Guid pageId)
	{
		var report = await _cardService.GetCardsXlsx(pageId);

		return File(report, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
	}

	[HttpPost("[action]")]
	public async Task<IActionResult> UpdateCardAsync(Guid id, CardBlank card)
	{
		var result = await _cardService.UpdateCardAsync(id, card, UserId);

		return result ? Ok() : BadRequest();
	}

	[HttpDelete("[action]")]
	public async Task<IActionResult> DeleteCardAsync(Guid id)
	{
		var result = await _cardService.DeleteCardAsync(id, UserId);

		return result ? Ok() : BadRequest();
	}

	[HttpPost("[action]")]
	public async Task<IActionResult> BlockCardAsync(BlockedCardBlank blockedCard)
	{
		var result = await _cardService.CreateBlockedCardAsync(blockedCard, UserId);

		return result ? Ok() : BadRequest();
	}

	[HttpPut("[action]")]
	public async Task<IActionResult> UpdateBlockedCardAsync(Guid cardId, BlockedCardBlank blockedCard)
	{
		var result = await _cardService.UpdateBlockedCardAsync(cardId, blockedCard, UserId);

		return result ? Ok() : BadRequest();
	}

	[HttpPost("[action]")]
	public async Task<IActionResult> UbBlockCardAsync(Guid cardId)
	{
		var result = await _cardService.DeleteBlockedCardAsync(cardId, UserId);

		return result ? Ok() : BadRequest();
	}
}