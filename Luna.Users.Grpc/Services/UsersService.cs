using Grpc.Core;
using Luna.Models.Users.Domain.Users;
using Luna.Models.Users.View.Users;
using Luna.Users.Grpc.Extensions;
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


	public override async Task<UsersList> GetUsersAsync(GetUsersAsyncRequest request, ServerCallContext context)
	{
		// var user = await _userRepository.GetUsers();
		//
		// var userDomain = new UserModel(user);
		//
		// var userView = new UserView(userDomain);
		//
		// return userView.ToUserModel();

		throw new NotImplementedException();
	}

	public async Task<UserModel?> GetUserByPhoneOrEmailAsync(string value)
	{
		throw new NotImplementedException();
	}

	public async Task<IEnumerable<UserModel>> GetUsers()
	{
		throw new NotImplementedException();
	}

	public async Task<IEnumerable<UserModel>> GetUsers(int offset)
	{
		throw new NotImplementedException();
	}

	public async Task<bool> CreateUserAsync(UserModel userDatabase)
	{
		throw new NotImplementedException();
	}

	public async Task<bool> UpdateUserAsync(Guid id, UserModel userDatabase)
	{
		throw new NotImplementedException();
	}

	public async Task<bool> DeleteUserAsync(Guid id)
	{
		throw new NotImplementedException();
	}

	public async Task<bool> DeleteUserAsync(string username)
	{
		throw new NotImplementedException();
	}
}