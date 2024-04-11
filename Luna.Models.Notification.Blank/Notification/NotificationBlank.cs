namespace Luna.Models.Notification.Blank.Notification;

public class NotificationBlank
{
	public Guid UserId { get; set; }

	public String Text { get; set; } = null!;

	public Int32 Priority { get; set; }

	public String? ImageUrl { get; set; }
}