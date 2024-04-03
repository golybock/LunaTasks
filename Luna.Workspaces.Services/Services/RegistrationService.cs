using System.Diagnostics;
using System.Text;
using Luna.Models.Workspace.Blank.Workspace;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Luna.Workspaces.Services.Services;

public class RegistrationService : BackgroundService
{
	private readonly IConnection _connection;
	private readonly IModel _channel;

	private readonly IWorkspaceService _workspaceService;

	public RegistrationService(IWorkspaceService workspaceService, IConfiguration configuration)
	{
		_workspaceService = workspaceService;

		var factory = new ConnectionFactory { HostName = "localhost" };
		_connection = factory.CreateConnection();
		_channel = _connection.CreateModel();

		_channel.QueueDeclare(queue: "MyQueue", durable: false, exclusive: false, autoDelete: false, arguments: null);
	}

	protected override Task ExecuteAsync(CancellationToken stoppingToken)
	{
		stoppingToken.ThrowIfCancellationRequested();

		var consumer = new EventingBasicConsumer(_channel);
		consumer.Received += async (ch, ea) =>
		{
			var content = Encoding.UTF8.GetString(ea.Body.ToArray());

			var userId = Guid.Parse(content);

			var blank = new WorkspaceBlank()
			{
				Name = "Empty workspace"
			};

			await _workspaceService.CreateWorkspaceAsync(blank, userId);

			Debug.WriteLine($"User created: {userId}");

			_channel.BasicAck(ea.DeliveryTag, false);
		};

		_channel.BasicConsume("MyQueue", false, consumer);

		return Task.CompletedTask;
	}

	public override void Dispose()
	{
		_channel.Close();
		_connection.Close();

		base.Dispose();
	}
}