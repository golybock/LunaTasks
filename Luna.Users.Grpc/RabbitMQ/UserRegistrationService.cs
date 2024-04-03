using System.Text;
using RabbitMQ.Client;

namespace Luna.Users.Grpc.RabbitMQ;

public class UserRegistrationService : IUserRegistrationService
{
	private string Host { get; }
	private string Queue { get; }

	public UserRegistrationService(IConfiguration configuration)
	{
		Host = configuration["RabbitMQHost"] ?? throw new ArgumentNullException("RabbitMQHost");
		Queue = configuration["WorkspaceQueue"] ?? throw new ArgumentNullException("WorkspaceQueue");
	}

	public void CreateWorkspace(Guid userid)
	{
		var factory = new ConnectionFactory { HostName = Host };
		using var connection = factory.CreateConnection();
		using var channel = connection.CreateModel();

		channel.QueueDeclare(queue: Queue,
			durable: false,
			exclusive: false,
			autoDelete: false,
			arguments: null);

		var body = Encoding.UTF8.GetBytes(userid.ToString());

		channel.BasicPublish(exchange: "",
			routingKey: Queue,
			basicProperties: null,
			body: body);
	}
}