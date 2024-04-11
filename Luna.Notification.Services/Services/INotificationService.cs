using Luna.Models.Notification.Blank.Notification;
using Luna.Notification.View.notification;
using Microsoft.AspNetCore.Mvc;

namespace Luna.Notification.Services.Services;

public interface INotificationService
{
	public Task<NotificationView?> GetNotificationByIdAsync(Guid id);

	public Task<IEnumerable<NotificationView>> GetNotificationsByIdsAsync(IEnumerable<Guid> ids);

	public Task<IEnumerable<NotificationView>> GetNotificationsAsync(Guid userId, Boolean withRead = false);

	public Task<IEnumerable<NotificationView>> GetNotificationsByCreatorAsync(Guid createdUserId);

	public Task<IActionResult> CreateNotificationAsync(NotificationBlank notificationBlank, Guid userId);

	public Task<IActionResult> UpdateNotificationAsync(Guid id, NotificationBlank notificationBlank);

	public Task<IActionResult> ReadNotificationsAsync(Guid id);

	public Task<IActionResult> UnReadNotificationsAsync(Guid id);

	public Task<IActionResult> ReadNotificationsAsync(IEnumerable<Guid> ids);

	public Task<IActionResult> UnReadNotificationsAsync(IEnumerable<Guid> ids);

	public Task<IActionResult> DeleteNotificationAsync(Guid id);

	public Task<Boolean> DeleteUserAsync(Guid userId);
}