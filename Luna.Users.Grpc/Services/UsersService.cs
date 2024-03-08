using Grpc.Core;
using Luna.Users.Grpc.Extensions;
using Luna.Users.Services.Services;

namespace Luna.Users.Grpc.Services;

public class UsersService : Grpc.UsersService.UsersServiceBase
{
	private readonly ILogger<UsersService> _logger;

	private readonly IUserService _userService;

	public UsersService(ILogger<UsersService> logger, IUserService userService)
	{
		_logger = logger;
		_userService = userService;
	}

	public override async Task<UsersList> GetUsers(GetUsersRequest request, ServerCallContext context)
	{
		var users = await _userService.GetUsersAsync();

		var usersModel = users.Select(u => u.ToUserModel());

		return new UsersList() {Users = {usersModel}};
	}

	public override async Task<UserResponse> GetUserById(GetUserRequest request, ServerCallContext context)
	{
		var user = await _userService.GetUserAsync(Guid.Parse(request.Id));

		if (user == null) return new UserResponse();

		return new UserResponse() {User = user.ToUserModel()};
	}

	public override async Task<UserResponse> GetUserByPhoneOrEmail(GetUserByPhoneOrEmailRequest request, ServerCallContext context)
	{
		var user = await _userService.GetUserByPhoneOrEmailAsync(request.Value);

		if (user == null) return new UserResponse();

		return new UserResponse() {User = user.ToUserModel()};
	}

	public override async Task<ExecutedResponse> CreateUser(CreateUserRequest request, ServerCallContext context)
	{
		var result = await _userService.CreateUserAsync(request.UserBlank.ToUserBlank());

		return new ExecutedResponse() {Executed = result};
	}

	public override async Task<ExecutedResponse> UpdateUser(UpdateUserRequest request, ServerCallContext context)
	{
		var result = await _userService.UpdateUserAsync(Guid.Parse(request.Id), request.UserBlank.ToUserBlank());

		return new ExecutedResponse() {Executed = result};
	}

	public override async Task<ExecutedResponse> DeleteUser(DeleteUserRequest request, ServerCallContext context)
	{
		var result = await _userService.DeleteUserAsync(request.Username);

		return new ExecutedResponse() {Executed = result};
	}

	public override async Task<ExecutedResponse> DeleteUserById(DeleteUserByIdRequest request, ServerCallContext context)
	{
		var result = await _userService.DeleteUserAsync(Guid.Parse(request.Id));

		return new ExecutedResponse() {Executed = result};
	}
}