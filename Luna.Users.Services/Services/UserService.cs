using Luna.Models.Users.Blank.Users;
using Luna.Models.Users.Database.Users;
using Luna.Models.Users.Domain.Users;
using Luna.Models.Users.View.Users;
using Luna.Users.Repositories.Repositories;

namespace Luna.Users.Services.Services;

public class UserService : IUserService
{
	private readonly IUserRepository _userRepository;

	public UserService(IUserRepository userRepository)
	{
		_userRepository = userRepository;
	}

	public async Task<UserView?> GetUserAsync(Guid id)
	{
		var user = await _userRepository.GetUserAsync(id);

		if (user == null)
			return null;

		var userDomain = new UserDomain(user);

		var userView = new UserView(userDomain);

		return userView;
	}

	public async Task<UserView?> GetUserByPhoneOrEmailAsync(string value)
	{
		var user = await _userRepository.GetUserByPhoneOrEmailAsync(value);

		if (user == null)
			return null;

		var userDomain = new UserDomain(user);

		var userView = new UserView(userDomain);

		return userView;
	}

	public async Task<IEnumerable<UserView>> GetUsersAsync()
	{
		var users = await _userRepository.GetUsers();

		var usersDomain = users.Select(u => new UserDomain(u));

		var usersView = usersDomain.Select(u => new UserView(u));

		return usersView;
	}

	public async Task<IEnumerable<UserView>> GetUsersAsync(int offset)
	{
		var users = await _userRepository.GetUsers(offset);

		var usersDomain = users.Select(u => new UserDomain(u));

		var usersView = usersDomain.Select(u => new UserView(u));

		return usersView;
	}

	public async Task<Guid> CreateUserAsync(UserBlank userBlank)
	{
		var userDatabase = new UserDatabase()
		{
			Id = Guid.NewGuid(),
			Username = userBlank.Username,
			Email = userBlank.Email,
			PhoneNumber = userBlank.PhoneNumber,
			Image = userBlank.Image,
			CreatedTimestamp = DateTime.UtcNow,
			EmailConfirmed = false
		};

		var res = await _userRepository.CreateUserAsync(userDatabase);

		return res ? userDatabase.Id : Guid.Empty;
	}

	public async Task<bool> UpdateUserAsync(Guid id, UserBlank userBlank)
	{
		var userDatabase = new UserDatabase()
		{
			Email = userBlank.Email,
			Username = userBlank.Username,
			PhoneNumber = userBlank.PhoneNumber,
			Image = userBlank.Image
		};

		Console.WriteLine(userBlank.Image);

		return await _userRepository.UpdateUserAsync(id, userDatabase);
	}

	public async Task<bool> DeleteUserAsync(Guid id)
	{
		return await _userRepository.DeleteUserAsync(id);
	}

	public async Task<bool> DeleteUserAsync(string username)
	{
		return await _userRepository.DeleteUserAsync(username);
	}
}