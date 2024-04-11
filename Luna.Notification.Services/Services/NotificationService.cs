using Luna.Models.Notification.Blank.Notification;
using Luna.Models.Notification.Database.Notifications;
using Luna.Models.Notification.Domain.Notification;
using Luna.Notification.Repositories.Repositories;
using Luna.Notification.View.notification;
using Luna.Tools.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Luna.Notification.Services.Services;

public class NotificationService : INotificationService
{
	private readonly INotificationRepository _notificationRepository;

	public NotificationService(INotificationRepository notificationRepository)
	{
		_notificationRepository = notificationRepository;
	}

	public async Task<NotificationView?> GetNotificationByIdAsync(Guid id)
	{
		var notification = await _notificationRepository.GetNotificationByIdAsync(id);

		if (notification == null)
			return null;

		return ToNotificationView(notification);
	}

	public async Task<IEnumerable<NotificationView>> GetNotificationsByIdsAsync(IEnumerable<Guid> ids)
	{
		var notifications = await _notificationRepository.GetNotificationsByIdsAsync(ids);

		return ToNotificationViews(notifications);
	}

	public async Task<IEnumerable<NotificationView>> GetNotificationsAsync(Guid userId, bool withRead = false)
	{
		var notifications = await _notificationRepository.GetNotificationsAsync(userId, withRead);

		return ToNotificationViews(notifications);
	}

	public async Task<IEnumerable<NotificationView>> GetNotificationsByCreatorAsync(Guid createdUserId)
	{
		var notifications = await _notificationRepository.GetNotificationsByCreatorAsync(createdUserId);

		return ToNotificationViews(notifications);
	}

	public async Task<IActionResult> CreateNotificationAsync(NotificationBlank notificationBlank, Guid userId)
	{
		var notificationDatabase = new NotificationDatabase()
		{
			Id = Guid.NewGuid(),
			Text = notificationBlank.Text,
			ImageUrl = notificationBlank.ImageUrl,
			Priority = notificationBlank.Priority,
			Created = DateTime.UtcNow,
			CreatedUser = userId,
			UserId = notificationBlank.UserId,
		};

		var result = await _notificationRepository.CreateNotificationAsync(notificationDatabase);

		return result ? new OkResult() : new BadRequestResult();
	}

	public async Task<IActionResult> UpdateNotificationAsync(Guid id, NotificationBlank notificationBlank)
	{
		var notificationDatabase = new NotificationDatabase()
		{
			Text = notificationBlank.Text,
			ImageUrl = notificationBlank.ImageUrl,
			Priority = notificationBlank.Priority
		};

		var result = await _notificationRepository.UpdateNotificationAsync(id, notificationDatabase);

		return result ? new OkResult() : new BadRequestResult();
	}

	public async Task<IActionResult> ReadNotificationsAsync(Guid id)
	{
		var result = await _notificationRepository.ReadNotificationsAsync(id);

		return result ? new OkResult() : new BadRequestResult();
	}

	public async Task<IActionResult> UnReadNotificationsAsync(Guid id)
	{
		var result = await _notificationRepository.UnReadNotificationsAsync(id);

		return result ? new OkResult() : new BadRequestResult();
	}

	public async Task<IActionResult> ReadNotificationsAsync(IEnumerable<Guid> ids)
	{
		var result = await _notificationRepository.ReadNotificationsAsync(ids);

		return result ? new OkResult() : new BadRequestResult();
	}

	public async Task<IActionResult> UnReadNotificationsAsync(IEnumerable<Guid> ids)
	{
		var result = await _notificationRepository.UnReadNotificationsAsync(ids);

		return result ? new OkResult() : new BadRequestResult();
	}

	public async Task<IActionResult> DeleteNotificationAsync(Guid id)
	{
		var result = await _notificationRepository.DeleteNotificationAsync(id);

		return result ? new OkResult() : new BadRequestResult();
	}

	public async Task<bool> DeleteUserAsync(Guid userId)
	{
		return await _notificationRepository.DeleteUserAsync(userId);
	}

	public NotificationView ToNotificationView(NotificationDatabase notificationDatabase)
	{
		var notificationDomain = new NotificationDomain()
		{
			Id = notificationDatabase.Id,
			Created = notificationDatabase.Created,
			CreatedUser = notificationDatabase.CreatedUser,
			Text = notificationDatabase.Text,
			ImageUrl = notificationDatabase.ImageUrl,
			Priority = (Priority) notificationDatabase.Priority,
			UserId = notificationDatabase.UserId,
			Read = notificationDatabase.Read,
			ReadAt = notificationDatabase.ReadAt
		};

		return new NotificationView(notificationDomain);
	}

	public IEnumerable<NotificationView> ToNotificationViews(IEnumerable<NotificationDatabase> notificationDatabases)
	{
		return notificationDatabases.Select(ToNotificationView);
	}
}