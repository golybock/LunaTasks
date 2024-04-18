namespace Luna.Models.Notification.Blank.Notification;

public class BackgroundNotificationBlank
{
	public Guid ByUserId { get; set; }

	public NotificationBlank NotificationBlank { get; set; } = null!;
}