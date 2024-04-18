using Grpc.Core;
using Luna.Models.Notification.Blank.Notification;
using Luna.SharedDataAccess.Notification.Services;
using Luna.Users.Grpc.Extensions;
using Luna.Users.Grpc.RabbitMQ;
using Luna.Users.Services.Services;

namespace Luna.Users.Grpc.Services;

public class UsersService : Grpc.UsersService.UsersServiceBase
{
	private readonly ILogger<UsersService> _logger;

	private readonly IUserService _userService;
	private readonly INotificationService _notificationService;

	private readonly IUserRegistrationService _registrationService;

	public UsersService(ILogger<UsersService> logger, IUserService userService,
		IUserRegistrationService registrationService, INotificationService notificationService)
	{
		_logger = logger;
		_userService = userService;
		_registrationService = registrationService;
		_notificationService = notificationService;
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

	public override async Task<UserResponse> GetUserByPhoneOrEmail(GetUserByPhoneOrEmailRequest request,
		ServerCallContext context)
	{
		var user = await _userService.GetUserByPhoneOrEmailAsync(request.Value);

		if (user == null) return new UserResponse();

		return new UserResponse() {User = user.ToUserModel()};
	}

	public override async Task<CreateUserResponse> CreateUser(CreateUserRequest request, ServerCallContext context)
	{
		var result = await _userService.CreateUserAsync(request.UserBlank.ToUserBlank());

		if (result != Guid.Empty)
		{
			_registrationService.CreateWorkspace(result);

			await _notificationService.MakeNotification(
				Guid.Empty,
				new NotificationBlank() {Text = "Welcome", Priority = 1, UserId = result}
			);
		}

		return new CreateUserResponse() {Id = result.ToString()};
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

	public override async Task<ExecutedResponse> DeleteUserById(DeleteUserByIdRequest request,
		ServerCallContext context)
	{
		var result = await _userService.DeleteUserAsync(Guid.Parse(request.Id));

		return new ExecutedResponse() {Executed = result};
	}
}