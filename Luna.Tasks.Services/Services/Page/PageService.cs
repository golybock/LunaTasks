using Luna.Models.Tasks.Blank.Page;
using Luna.Models.Tasks.Database.Page;
using Luna.Models.Tasks.Domain.Card;
using Luna.Models.Tasks.Domain.Page;
using Luna.Models.Tasks.View.Page;
using Luna.Tasks.Repositories.Repositories.Card;
using Luna.Tasks.Repositories.Repositories.Page;

namespace Luna.Tasks.Services.Services.Page;

// todo add users and more completely
public class PageService : IPageService
{
	private readonly IPageRepository _pageRepository;
	private readonly ICardRepository _cardRepository;

	public PageService(IPageRepository pageRepository, ICardRepository cardRepository)
	{
		_pageRepository = pageRepository;
		_cardRepository = cardRepository;
	}

	public async Task<IEnumerable<PageView>> GetWorkspacePagesAsync(Guid workspaceId)
	{
		var pages = await _pageRepository.GetWorkspacePagesAsync(workspaceId);

		return ToPageViews(pages);
	}

	public async Task<IEnumerable<PageView>> GetPagesByUserAsync(Guid userId)
	{
		var pages = await _pageRepository.GetPagesByUserAsync(userId);

		return ToPageViews(pages);
	}

	public async Task<PageView?> GetPageAsync(Guid id)
	{
		var page = await _pageRepository.GetPageAsync(id);

		if (page == null)
			return null;

		return ToPageView(page);
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
	private PageView ToPageView(PageDatabase pageDatabase)
	{
		var pageDomain = new PageDomain(pageDatabase, new List<CardDomain>());
		var pageView = new PageView(pageDomain);

		return pageView;
	}

	private IEnumerable<PageView> ToPageViews(IEnumerable<PageDatabase> pageDatabases)
	{
		return pageDatabases.Select(ToPageView).ToList();
	}
}