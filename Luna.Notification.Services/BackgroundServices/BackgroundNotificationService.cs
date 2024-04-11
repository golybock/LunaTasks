using System.Text;
using System.Text.Json;
using Luna.Models.Notification.Blank.Notification;
using Luna.Notification.Services.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Luna.Notification.Services.BackgroundServices;

public class BackgroundNotificationService : BackgroundService
{
	private readonly IConnection _connection;
	private readonly IModel _channel;

	private string Host { get; }
	private string Queue { get; }

	private readonly INotificationService _notificationService;

	public BackgroundNotificationService(INotificationService notificationService, IConfiguration configuration)
	{
		_notificationService = notificationService;

		Host = configuration["RabbitMQHost"] ?? throw new ArgumentNullException("RabbitMQHost");
		Queue = configuration["NotificationQueue"] ?? throw new ArgumentNullException("NotificationQueue");

		var factory = new ConnectionFactory { HostName = Host };
		_connection = factory.CreateConnection();
		_channel = _connection.CreateModel();

		_channel.QueueDeclare(queue: Queue, durable: false, exclusive: false, autoDelete: false, arguments: null);
	}

	protected override Task ExecuteAsync(CancellationToken stoppingToken)
	{
		var consumer = new EventingBasicConsumer(_channel);

		consumer.Received += async (ch, ea) =>
		{
			var content = Encoding.UTF8.GetString(ea.Body.ToArray());

			var blank = JsonSerializer.Deserialize<BackgroundNotificationBlank>(content);

			await _notificationService.CreateNotificationAsync(blank.NotificationBlank, blank.ByUserId);
		};

		_channel.BasicConsume(Queue, true, consumer);

		return Task.CompletedTask;
	}

	public override void Dispose()
	{
		_channel.Close();
		_connection.Close();

		base.Dispose();
	}
}