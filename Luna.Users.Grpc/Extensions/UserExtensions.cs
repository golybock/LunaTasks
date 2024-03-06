using Google.Protobuf.WellKnownTypes;
using Luna.Models.Users.View.Users;

namespace Luna.Users.Grpc.Extensions;

public static class UserExtensions
{
	public static UserModel ToUserModel(this UserView userView)
	{
		return new UserModel()
		{
			Id = userView.Id.ToString(),
			Username = userView.Username,
			PhoneNumber = userView.PhoneNumber,
			Email = userView.Email,
			CreatedTimestamp = Timestamp.FromDateTime(userView.CreatedTimestamp),
			EmailConfirmed = userView.EmailConfirmed,
		};
	}
}