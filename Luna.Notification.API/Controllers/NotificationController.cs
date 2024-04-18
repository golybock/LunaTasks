using Luna.Models.Notification.Blank.Notification;
using Luna.Notification.Services.Services;
using Luna.Notification.View.notification;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ControllerBase = Luna.Tools.Web.ControllerBase;

namespace Luna.Notification.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class NotificationController : ControllerBase
{
	private readonly INotificationService _notificationService;

	public NotificationController(INotificationService notificationService)
	{
		_notificationService = notificationService;
	}

	[HttpGet("[action]")]
	public async Task<IEnumerable<NotificationView>> GetNotificationsAsync(bool withRead = false)
	{
		return await _notificationService.GetNotificationsAsync(UserId, withRead);
	}

	[HttpGet("[action]")]
	public async Task<IEnumerable<NotificationView>> GetNotificationsByCreatorAsync(Guid createdUserId)
	{
		return await _notificationService.GetNotificationsByCreatorAsync(createdUserId);
	}

	[HttpPost("[action]")]
	public async Task<IActionResult> CreateNotificationAsync(NotificationBlank notificationBlank)
	{
		return await _notificationService.CreateNotificationAsync(notificationBlank, UserId);
	}

	[HttpPut("[action]")]
	public async Task<IActionResult> UpdateNotificationAsync(Guid id, NotificationBlank notificationBlank)
	{
		return await _notificationService.UpdateNotificationAsync(id, notificationBlank);
	}

	[HttpPost("[action]")]
	public async Task<IActionResult> ReadNotificationAsync(Guid id)
	{
		return await _notificationService.ReadNotificationsAsync(id);
	}

	[HttpPost("[action]")]
	public async Task<IActionResult> UnReadNotificationAsync(Guid id)
	{
		return await _notificationService.UnReadNotificationsAsync(id);
	}

	[HttpPost("[action]")]
	public async Task<IActionResult> ReadNotificationsAsync(IEnumerable<Guid> ids)
	{
		return await _notificationService.ReadNotificationsAsync(ids);
	}

	[HttpPost("[action]")]
	public async Task<IActionResult> UnReadNotificationsAsync(IEnumerable<Guid> ids)
	{
		return await _notificationService.UnReadNotificationsAsync(ids);
	}

	[HttpPost("[action]")]
	public async Task<IActionResult> DeleteNotificationAsync(Guid id)
	{
		return await _notificationService.DeleteNotificationAsync(id);
	}
}