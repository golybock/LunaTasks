using Luna.Models.Tasks.Blank.Page;
using Luna.Models.Tasks.Database.Page;
using Luna.Models.Tasks.Domain.Card;
using Luna.Models.Tasks.Domain.Page;
using Luna.Models.Tasks.View.Page;
using Luna.SharedDataAccess.Users.Services;
using Luna.Tasks.Repositories.Repositories.Card;
using Luna.Tasks.Repositories.Repositories.Page;
using Luna.Tasks.Services.Services.Card;
using Microsoft.AspNetCore.Mvc;

namespace Luna.Tasks.Services.Services.Page;

// todo add users and more completely
public class PageService : IPageService
{
	private readonly IPageRepository _pageRepository;
	private readonly IUserService _userService;

	public PageService(IPageRepository pageRepository,IUserService userService)
	{
		_pageRepository = pageRepository;
		_userService = userService;
	}

	private async Task<PageDomain> GetPageDomain(PageDatabase pageDatabase)
	{
		var user = await _userService.GetUserDomainAsync(pageDatabase.CreatedUserId);

		var pageDomain = new PageDomain(pageDatabase, user ?? throw new ArgumentNullException(nameof(user)));

		return pageDomain;
	}

	public async Task<IEnumerable<PageView>> GetWorkspacePagesAsync(Guid workspaceId, bool deleted = false)
	{
		var pages = await _pageRepository.GetWorkspacePagesAsync(workspaceId, deleted);

		var pagesView = new List<PageView>(pages.Count());

		foreach (var page in pages)
		{
			var pageDomain = await GetPageDomain(page);
			pagesView.Add(ToPageView(pageDomain));
		}

		return pagesView;
	}

	public async Task<IEnumerable<PageView>> GetPagesByUserAsync(Guid userId, bool deleted = false)
	{
		var pages = await _pageRepository.GetPagesByUserAsync(userId, deleted);

		var pagesView = new List<PageView>(pages.Count());

		foreach (var page in pages)
		{
			var pageDomain = await GetPageDomain(page);
			pagesView.Add(ToPageView(pageDomain));
		}

		return pagesView;
	}

	public async Task<PageView?> GetPageAsync(Guid id)
	{
		var page = await _pageRepository.GetPageAsync(id);

		if (page == null)
			return null;

		var pageDomain = await GetPageDomain(page);

		return ToPageView(pageDomain);
	}

	public async Task<IActionResult> CreatePageAsync(PageBlank page, Guid userId)
	{
		var pageDatabase = ToPageDatabase(page, userId);

		var result = await _pageRepository.CreatePageAsync(pageDatabase);

		return result ? new OkObjectResult("Успешно создано") : new BadRequestObjectResult("Ошибка создания");
	}

	public async Task<IActionResult> UpdatePageAsync(Guid id, PageBlank page, Guid userId)
	{
		var pageDatabase = ToPageDatabase(page, userId);

		var result = await _pageRepository.UpdatePageAsync(id, pageDatabase);

		return result ? new OkObjectResult("Успешно обновлено") : new BadRequestObjectResult("Ошибка обновления");
	}

	public async Task<IActionResult> ToTrashPageAsync(Guid id, Guid userId)
	{
		var page = await _pageRepository.GetPageAsync(id);

		if (page == null)
			return new NotFoundResult();

		if (page.CreatedUserId != userId)
			return new BadRequestObjectResult("Карточку может удалить только ее создатель");

		var result = await _pageRepository.ToTrashPageAsync(id);

		return result ? new OkObjectResult("Успешно удалено") : new BadRequestObjectResult("Ошибка удаления");
	}

	public async Task<IActionResult> DeletePageAsync(Guid id, Guid userId)
	{
		var page = await _pageRepository.GetPageAsync(id);

		if (page == null)
			return new NotFoundResult();

		var result = await _pageRepository.DeletePageAsync(id);

		return result ? new OkObjectResult("Успешно удалено") : new BadRequestObjectResult("Ошибка удаления");
	}

	public async Task<Boolean> DeleteWorkspacePagesAsync(Guid workspaceId)
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