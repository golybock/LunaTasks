using System.Security.Claims;
using Luna.Auth.Repositories.Repositories;
using Luna.Models.Auth.Blank.Auth;
using Luna.Models.Auth.Domain.Auth;
using Luna.SharedDataAccess.Users.Services;
using Luna.Tools.Crypto;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Luna.Auth.Services.Services;

public class AuthService: IAuthService
{
	private IUserAuthRepository _userAuthRepository;
	private IUserService _userService;

	public AuthService(IUserAuthRepository userAuthRepository, IUserService userService)
	{
		_userAuthRepository = userAuthRepository;
		_userService = userService;
	}

	public async Task<IActionResult> SignIn(SignInBlank signInBlank, HttpContext context)
	{
		var user = await _userService.GetUserAsync(signInBlank.Email);

		if (user == null)
			return new NotFoundResult();

		var auth = await _userAuthRepository.GetAuthUserByIdAsync(user.Id);

		if (auth == null)
			return new NotFoundResult();

		var authDomain = new UserAuthDomain(auth);

		var hashedPassword = await Crypto.HashSha512Async(signInBlank.Password);

		if (!authDomain.PasswordIsValid(hashedPassword))
			return new UnauthorizedResult();

		var claims = new List<Claim>
		{
			new Claim(ClaimTypes.Authentication, user.Id.ToString()),
			new Claim(ClaimTypes.Name, user.Email)
			// new Claim(ClaimTypes.Role, "Administrator"),
		};

		var claimsIdentity = new ClaimsIdentity(claims);

		var authProperties = new AuthenticationProperties
		{
			AllowRefresh = true,
		};

		await context.SignInAsync(new ClaimsPrincipal(claimsIdentity), authProperties);

		return new OkResult();
	}

	public async Task<IActionResult> SignUp(SignUpBlank userBlank, HttpContext context)
	{
		throw new NotImplementedException();
	}

	public async Task<IActionResult> SignOut(HttpContext context)
	{
		await context.SignOutAsync();

		return new OkResult();
	}
}