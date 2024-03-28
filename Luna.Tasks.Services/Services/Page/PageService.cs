using Luna.Models.Tasks.Blank.Page;
using Luna.Models.Tasks.Database.Page;
using Luna.Models.Tasks.Domain.Card;
using Luna.Models.Tasks.Domain.Page;
using Luna.Models.Tasks.View.Page;
using Luna.Tasks.Repositories.Repositories.Card;
using Luna.Tasks.Repositories.Repositories.Page;
using Luna.Tasks.Services.Services.Card;

namespace Luna.Tasks.Services.Services.Page;

// todo add users and more completely
public class PageService : IPageService
{
	private readonly IPageRepository _pageRepository;
	private readonly ICardService _cardService;

	public PageService(IPageRepository pageRepository, ICardService cardService)
	{
		_pageRepository = pageRepository;
		_cardService = cardService;
	}

	public async Task<IEnumerable<PageView>> GetWorkspacePagesAsync(Guid workspaceId)
	{
		var pages = await _pageRepository.GetWorkspacePagesAsync(workspaceId);

		var pagesView = new List<PageView>();

		foreach (var page in pages)
		{
			var pageDomain = await GetPageDomain(page);
			pagesView.Add(ToPageView(pageDomain));
		}

		return pagesView;
	}

	public async Task<IEnumerable<PageView>> GetPagesByUserAsync(Guid userId)
	{
		var pages = await _pageRepository.GetPagesByUserAsync(userId);

		var pagesView = new List<PageView>();

		foreach (var page in pages)
		{
			var pageDomain = await GetPageDomain(page);
			pagesView.Add(ToPageView(pageDomain));
		}

		return pagesView;
	}

	// todo get only card preview
	private async Task<PageDomain> GetPageDomain(PageDatabase pageDatabase)
	{
		// var cards = await _cardService.GetCardsDomainAsync(pageDatabase.Id);

		var pageDomain = new PageDomain(pageDatabase, new List<CardDomain>());

		return pageDomain;
	}

	public async Task<PageView?> GetPageAsync(Guid id)
	{
		var page = await _pageRepository.GetPageAsync(id);

		if (page == null)
			return null;

		var pageDomain = await GetPageDomain(page);

		return ToPageView(pageDomain);
	}

	public async Task<bool> CreatePageAsync(PageBlank page, Guid userId)
	{
		var pageDatabase = ToPageDatabase(page, userId);

		var result = await _pageRepository.CreatePageAsync(pageDatabase);

		return result;
	}

	public async Task<bool> UpdatePageAsync(Guid id, PageBlank page, Guid userId)
	{
		var pageDatabase = ToPageDatabase(page, userId);

		var result = await _pageRepository.UpdatePageAsync(id, pageDatabase);

		return result;
	}

	public async Task<bool> DeletePageAsync(Guid id, Guid userId)
	{
		var result = await _pageRepository.DeletePageAsync(id);

		return result;
	}

	public async Task<bool> DeleteWorkspacePagesAsync(Guid workspaceId)
	{
		var result = await _pageRepository.DeleteWorkspacePagesAsync(workspaceId);

		return result;
	}

	private PageDatabase ToPageDatabase(PageBlank pageBlank, Guid userId)
	{
		return new PageDatabase()
		{
			Id = Guid.NewGuid(),
			Description = pageBlank.Description,
			CreatedTimestamp = DateTime.UtcNow,
			Name = pageBlank.Name,
			CreatedUserId = userId,
			HeaderImage = pageBlank.HeaderImage,
			WorkspaceId = pageBlank.WorkspaceId.Value
		};
	}

	// todo add cards
	private PageView ToPageView(PageDomain pageDomain)
	{
		var pageView = new PageView(pageDomain);

		return pageView;
	}

	private IEnumerable<PageView> ToPageViews(IEnumerable<PageDomain> pageDomains)
	{
		return pageDomains.Select(ToPageView).ToList();
	}
}