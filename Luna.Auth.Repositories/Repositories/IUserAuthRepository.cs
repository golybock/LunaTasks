using Luna.Models.Auth.Database.Auth;

namespace Luna.Auth.Repositories.Repositories;

public interface IUserAuthRepository
{
	public Task<UserAuthDatabase?> GetAuthUserAsync(Guid userId);

	public Task<UserAuthDatabase?> GetAuthUserByIdAsync(Guid id);

	public Task<Boolean> CreateAuthUserAsync(UserAuthDatabase userAuthDatabase);

	public Task<Boolean> UpdateAuthUserAsync(Guid userId, UserAuthDatabase userAuthDatabase);

	public Task<Boolean> UpdateAuthUserByIdAsync(Guid id, UserAuthDatabase userAuthDatabase);

	public Task<Boolean> DeleteAuthUserByIdAsync(Guid id);

	public Task<Boolean> DeleteAuthUserAsync(Guid userId);
}