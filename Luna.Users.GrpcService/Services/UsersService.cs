using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
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

	public override async Task<UsersList> GetUsersAsync(GetUsersAsyncRequest request, ServerCallContext context)
	{
		var users = await _userRepository.GetUsers();

		var grpcUsers = users.Select(ToGrpcUserDatabase);

		var result = new RepeatedField<UserDatabase>();

		result.AddRange(grpcUsers);

		return new UsersList() {Users = {result}};
	}

	public override async Task<UserResponse> GetUserByIdAsync(GetUserAsyncRequest request, ServerCallContext context)
	{
		var id = Guid.Parse(request.Id);

		var user = await _userRepository.GetUserAsync(id);

		if (user == null)
			return new UserResponse();

		var result = new UserResponse() {User = ToGrpcUserDatabase(user)};

		return result;
	}

	private UserDatabase ToGrpcUserDatabase(SharedModels.Database.Users.UserDatabase userDatabase)
	{
		return new UserDatabase()
		{
			Id = userDatabase.Id.ToString(),
			Email = userDatabase.Email,
			CreatedTimestamp = Timestamp.FromDateTime(userDatabase.CreatedTimestamp),
			Username = userDatabase.Username,
			EmailConfirmed = userDatabase.EmailConfirmed,
			PhoneNumber = userDatabase.PhoneNumber
		};
	}
}