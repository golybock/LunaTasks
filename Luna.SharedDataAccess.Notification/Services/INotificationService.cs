using Luna.Models.Notification.Blank.Notification;

namespace Luna.SharedDataAccess.Notification.Services;

public interface INotificationService
{
	public Task MakeNotification(Guid byUserId, NotificationBlank notificationBlank);
}