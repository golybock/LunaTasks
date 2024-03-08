using Google.Protobuf.WellKnownTypes;
using Luna.Models.Users.View.Users;

namespace Luna.Users.Grpc.Extensions;

public static class UserExtensions
{
	public static UserModel ToUserModel(this UserView userView)
	{
		var model = new UserModel()
		{
			Id = userView.Id.ToString(),
			Username = userView.Username,
			Email = userView.Email,
			CreatedTimestamp = Timestamp.FromDateTime(userView.CreatedTimestamp),
			EmailConfirmed = userView.EmailConfirmed,
		};

		if (userView.PhoneNumber != null)
			model.PhoneNumber = userView.PhoneNumber;

		if (userView.Image != null)
			model.Image = userView.Image;

		return model;
	}

	public static Models.Users.Blank.Users.UserBlank ToUserBlank(this UserBlank userBlank)
	{
		return new Models.Users.Blank.Users.UserBlank()
		{
			Email = userBlank.Email,
			PhoneNumber = userBlank.PhoneNumber,
			Username = userBlank.Username,
			Image = userBlank.Image
		};
	}
}