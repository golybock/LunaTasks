using Luna.Models.Notification.Database.Notifications;

namespace Luna.Notification.Repositories.Repositories;

public interface INotificationRepository
{
	// todo add offset
	public Task<NotificationDatabase?> GetNotificationByIdAsync(Guid id);

	public Task<IEnumerable<NotificationDatabase>> GetNotificationsByIdsAsync(IEnumerable<Guid> ids);

	public Task<IEnumerable<NotificationDatabase>> GetNotificationsAsync(Guid userId, Boolean withRead = false);

	public Task<IEnumerable<NotificationDatabase>> GetNotificationsByCreatorAsync(Guid createdUserId);

	public Task<Boolean> CreateNotificationAsync(NotificationDatabase notificationDatabase);

	public Task<Boolean> UpdateNotificationAsync(Guid id, NotificationDatabase notificationDatabase);

	public Task<Boolean> ReadNotificationsAsync(Guid id);

	public Task<Boolean> UnReadNotificationsAsync(Guid id);

	public Task<Boolean> ReadNotificationsAsync(IEnumerable<Guid> ids);

	public Task<Boolean> UnReadNotificationsAsync(IEnumerable<Guid> ids);

	public Task<Boolean> DeleteNotificationAsync(Guid id);

	public Task<Boolean> DeleteUserAsync(Guid userId);
}