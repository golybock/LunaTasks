using Luna.Users.Repositories.Repositories;

namespace Luna.Users.Grpc.Services;

public class UsersService: Grpc.UsersService.UsersServiceBase
{
	private readonly ILogger<UsersService> _logger;
	private readonly IUserRepository _userRepository;

	public UsersService(ILogger<UsersService> logger, IUserRepository userRepository)
	{
		_logger = logger;
		_userRepository = userRepository;
	}


}