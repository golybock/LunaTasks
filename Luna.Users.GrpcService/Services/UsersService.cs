using Luna.Users.Repositories.Repositories;

namespace Luna.Users.GrpcService.Services;

public class UsersService : GrpcService.UsersService.UsersServiceBase
{
	private readonly ILogger<UsersService> _logger;
	private readonly IUserRepository _userRepository;

	public UsersService(ILogger<UsersService> logger, IUserRepository userRepository)
	{
		_logger = logger;
		_userRepository = userRepository;
	}


}