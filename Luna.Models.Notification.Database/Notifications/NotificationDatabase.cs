namespace Luna.Models.Notification.Database.Notifications;

public class NotificationDatabase
{
	public Guid Id { get; set; }

	public Guid UserId { get; set; }

	public String Text { get; set; } = null!;

	public DateTime Created { get; set; }

	public Guid CreatedUser { get; set; }

	public Int32 Priority { get; set; }

	public String? ImageUrl { get; set; }

	public Boolean Read { get; set; }

	public DateTime? ReadAt { get; set; }
}