using Luna.Auth.Repositories.Repositories;
using Luna.Models.Auth.Blank.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Luna.Auth.Services.Services;

public class AuthService: IAuthService
{
	private IUserAuthRepository _userAuthRepository;

	public AuthService(IUserAuthRepository userAuthRepository)
	{
		_userAuthRepository = userAuthRepository;
	}

	public async Task<IActionResult> SignIn(SignInBlank signInBlank, HttpContext context)
	{
		throw new NotImplementedException();
	}

	public async Task<IActionResult> SignUp(SignUpBlank userBlank, HttpContext context)
	{
		throw new NotImplementedException();
	}

	public async Task<IActionResult> SignOut(HttpContext context)
	{
		throw new NotImplementedException();
	}
}