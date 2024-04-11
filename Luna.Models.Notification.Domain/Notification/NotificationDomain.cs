using Luna.Tools.Enums;

namespace Luna.Models.Notification.Domain.Notification;

public class NotificationDomain
{
	public Guid Id { get; set; }

	public Guid UserId { get; set; }

	public String Text { get; set; } = null!;

	public DateTime Created { get; set; }

	public Guid CreatedUser { get; set; }

	public Priority Priority { get; set; }

	public String? ImageUrl { get; set; }

	public Boolean Read { get; set; }

	public DateTime? ReadAt { get; set; }
}