using System.Text;
using RabbitMQ.Client;

namespace Luna.Users.Grpc.RabbitMQ;

public class UserRegistrationService : IUserRegistrationService
{
	public void CreateWorkspace(Guid userid)
	{
		var factory = new ConnectionFactory() { HostName = "localhost" };
		using var connection = factory.CreateConnection();
		using var channel = connection.CreateModel();
		channel.QueueDeclare(queue: "MyQueue",
			durable: false,
			exclusive: false,
			autoDelete: false,
			arguments: null);

		var body = Encoding.UTF8.GetBytes(userid.ToString());

		channel.BasicPublish(exchange: "",
			routingKey: "MyQueue",
			basicProperties: null,
			body: body);
	}
}