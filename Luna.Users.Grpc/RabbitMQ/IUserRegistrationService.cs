namespace Luna.Users.Grpc.RabbitMQ;

public interface IUserRegistrationService
{
	void CreateWorkspace(Guid userid);
}