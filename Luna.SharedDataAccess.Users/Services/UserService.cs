using Grpc.Net.Client;
using Luna.Models.Users.View.Users;
using Luna.SharedDataAccess.Users.Extensions;
using Luna.Tools.gRPC;
using Luna.Users.Grpc;
using Microsoft.Extensions.Configuration;
using UserBlank = Luna.Models.Users.Blank.Users.UserBlank;

namespace Luna.SharedDataAccess.Users.Services;

public class UserService : GrpcServiceBase, IUserService
{
	public UserService(IConfiguration configuration) : base(configuration) { }

	public UserService(string host) : base(host) { }

	public async Task<IEnumerable<UserView>> GetUsersAsync()
	{
		var client = GetClient();

		var response = await client.GetUsersAsync(new GetUsersRequest());

		return response.Users.ToUsersView();
	}

	public async Task<UserView?> GetUserAsync(Guid id)
	{
		var client = GetClient();

		var response = await client.GetUserByIdAsync(new GetUserRequest(){Id = id.ToString()});

		if (response.User == null)
			return null;

		return response.User.ToUserView();
	}

	public async Task<UserView?> GetUserAsync(string phoneOrEmail)
	{
		var client = GetClient();

		var response = await client.GetUserByPhoneOrEmailAsync(new GetUserByPhoneOrEmailRequest() {Value = phoneOrEmail});

		if (response.User == null)
			return null;

		return response.User.ToUserView();
	}

	public async Task<Guid> CreateUserAsync(UserBlank userBlank)
	{
		var client = GetClient();

		var blank = userBlank.ToGrpcUserBlank();

		var response = await client.CreateUserAsync(new CreateUserRequest() {UserBlank = blank});

		return Guid.Parse(response.Id);
	}

	public async Task<bool> UpdateUserAsync(Guid id, UserBlank userBlank)
	{
		var client = GetClient();

		var blank = userBlank.ToGrpcUserBlank();

		var response = await client.UpdateUserAsync(new UpdateUserRequest() {Id = id.ToString(), UserBlank = blank});

		return response.Executed;
	}

	public async Task<bool> DeleteUserAsync(Guid id)
	{
		var client = GetClient();

		var response = await client.DeleteUserByIdAsync(new DeleteUserByIdRequest() {Id = id.ToString()});

		return response.Executed;
	}

	public async Task<bool> DeleteUserAsync(string username)
	{
		var client = GetClient();

		var response = await client.DeleteUserAsync(new DeleteUserRequest() {Username = username});

		return response.Executed;
	}

	private UsersService.UsersServiceClient GetClient()
	{
		var channel = GrpcChannel.ForAddress(Host);

		return new UsersService.UsersServiceClient(channel);
	}
}