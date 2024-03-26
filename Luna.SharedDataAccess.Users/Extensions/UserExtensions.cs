using Luna.Models.Users.Domain.Users;
using Luna.Models.Users.View.Users;
using Luna.Users.Grpc;

namespace Luna.SharedDataAccess.Users.Extensions;

public static class UserExtensions
{
	public static UserView ToUserView(this UserModel userModel)
	{
		return new UserView
		(
			Guid.Parse(userModel.Id),
			userModel.Username,
			userModel.Email,
			userModel.PhoneNumber,
			userModel.CreatedTimestamp.ToDateTime(),
			userModel.EmailConfirmed,
			userModel.Image
		);
	}

	public static UserDomain ToUserDomain(this UserModel userModel)
	{
		return new UserDomain
		(
			Guid.Parse(userModel.Id),
			userModel.Username,
			userModel.Email,
			userModel.PhoneNumber,
			userModel.CreatedTimestamp.ToDateTime(),
			userModel.EmailConfirmed,
			userModel.Image
		);
	}

	public static IEnumerable<UserView> ToUsersView(this IEnumerable<UserModel> userModels)
	{
		return userModels
			.Select(u => u.ToUserView())
			.ToList();
	}

	public static IEnumerable<UserDomain> ToUsersDomain(this IEnumerable<UserModel> userModels)
	{
		return userModels
			.Select(u => u.ToUserDomain())
			.ToList();
	}

	public static UserBlank ToGrpcUserBlank(this Models.Users.Blank.Users.UserBlank userBlank)
	{
		var model = new UserBlank()
		{
			Email = userBlank.Email,
			Username = userBlank.Username,
		};

		if (userBlank.PhoneNumber != null)
			model.PhoneNumber = userBlank.PhoneNumber;

		if (userBlank.Image != null)
			model.Image = userBlank.Image;

		return model;
	}
}