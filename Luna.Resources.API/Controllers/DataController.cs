using Luna.Data.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ControllerBase = Luna.Tools.Web.ControllerBase;

namespace Luna.Resources.API.Controllers;

[ApiController]
[Route("/")]
public class DataController : ControllerBase
{
	private readonly IDataService _dataService;

	public DataController(IDataService dataService)
	{
		_dataService = dataService;
	}

	[HttpGet("/{id:guid}")]
	public async Task<IActionResult> GetFileAsync(Guid id)
	{
		var file = await _dataService.GetFileAsync(id);

		if (file == null)
			return NotFound();

		return File(file, "image/jpeg");
	}

	[Authorize]
	[HttpGet("[action]")]
	public async Task<IEnumerable<String>> GetFilesAsync(Guid? workspaceId, Boolean deleted = false)
	{
		if (workspaceId == null)
		{
			return await _dataService.GetFilesAsync(UserId, deleted);
		}

		return await _dataService.GetFilesAsync(workspaceId.Value, UserId, deleted);
	}

	[Authorize]
	[HttpPost("[action]")]
	public async Task<Boolean> CreateFileAsyncAsync(IFormFile file, Guid workspaceId)
	{
		return await _dataService.CreateFileAsyncAsync(file, workspaceId, UserId);
	}

	[Authorize]
	[HttpDelete("[action]")]
	public async Task<Boolean> DeleteFileAsync(Guid id)
	{
		return await _dataService.DeleteFileAsync(id);
	}
}