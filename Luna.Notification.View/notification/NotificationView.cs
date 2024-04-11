using Luna.Models.Notification.Domain.Notification;
using Luna.Tools.Enums;

namespace Luna.Notification.View.notification;

public class NotificationView
{
	public Guid Id { get; set; }

	public String Text { get; set; }

	public DateTime Created { get; set; }

	public Guid CreatedUser { get; set; }

	public Priority Priority { get; set; }

	public String? ImageUrl { get; set; }

	public Boolean Read { get; set; }

	public DateTime? ReadAt { get; set; }

	public NotificationView(NotificationDomain notificationDomain)
	{
		Id = notificationDomain.Id;
		Text = notificationDomain.Text;
		Created = notificationDomain.Created;
		CreatedUser = notificationDomain.CreatedUser;
		Priority = notificationDomain.Priority;
		ImageUrl = notificationDomain.ImageUrl;
		Read = notificationDomain.Read;
		ReadAt = notificationDomain.ReadAt;
	}
}