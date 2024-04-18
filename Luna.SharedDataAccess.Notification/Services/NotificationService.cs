using System.Text;
using System.Text.Json;
using Luna.Models.Notification.Blank.Notification;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace Luna.SharedDataAccess.Notification.Services;

public class NotificationService : INotificationService
{
	private string Host { get; }
	private string Queue { get; }

	public NotificationService(IConfiguration configuration)
	{
		Host = configuration["RabbitMQHost"] ?? throw new ArgumentNullException("RabbitMQHost");
		Queue = configuration["NotificationQueue"] ?? throw new ArgumentNullException("NotificationQueue");
	}

	public async Task MakeNotification(Guid byUserId, NotificationBlank notificationBlank)
	{
		var factory = new ConnectionFactory {HostName = Host};
		using var connection = factory.CreateConnection();
		using var channel = connection.CreateModel();

		channel.QueueDeclare(
			queue: Queue,
			durable: false,
			exclusive: false,
			autoDelete: false,
			arguments: null
		);

		var blank = new BackgroundNotificationBlank()
		{
			ByUserId = byUserId,
			NotificationBlank = notificationBlank
		};

		var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(blank));

		channel.BasicPublish(
			exchange: "",
			routingKey: Queue,
			basicProperties: null,
			body: body
		);
	}
}