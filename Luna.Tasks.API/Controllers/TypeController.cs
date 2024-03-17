using Luna.Models.Tasks.Blank.CardAttributes;
using Luna.Models.Tasks.View.CardAttributes;
using Luna.Tasks.Services.Services.CardAttributes.Type;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ControllerBase = Luna.Tools.Web.ControllerBase;

namespace Luna.Tasks.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TypeController : ControllerBase
{
	private readonly ITypeService _typeService;

	public TypeController(ITypeService typeService)
	{
		_typeService = typeService;
	}

	[HttpGet("[action]")]
	public async Task<IEnumerable<TypeView>> GetTypesAsync(Guid workspaceId)
	{
		return await _typeService.GetTypesAsync(workspaceId);
	}

	[HttpGet("[action]")]
	public async Task<TypeView?> GetTypeAsync(Guid typeId)
	{
		return await _typeService.GetTypeAsync(typeId);
	}

	[HttpPost("[action]")]
	public async Task<IActionResult> CreateTypeAsync(TypeBlank type)
	{
		var result = await _typeService.CreateTypeAsync(type, UserId);

		return result ? Ok() : BadRequest();
	}

	[HttpPut("[action]")]
	public async Task<IActionResult> UpdateTypeAsync(Guid id, TypeBlank type)
	{
		var result = await _typeService.UpdateTypeAsync(id, type, UserId);

		return result ? Ok() : BadRequest();
	}

	[HttpDelete("[action]")]
	public async Task<IActionResult> DeleteTypeAsync(Guid id)
	{
		var result = await _typeService.DeleteTypeAsync(id);

		return result ? Ok() : BadRequest();
	}
}