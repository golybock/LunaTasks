using Luna.Auth.Services.Services;
using Luna.Models.Auth.Blank.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Luna.Auth.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
	private IAuthService _authService;

	public AuthController(IAuthService authService)
	{
		_authService = authService;
	}

	[HttpPost("[action]")]
	public async Task<IActionResult> SignIn(SignInBlank signInBlank)
	{
		return await _authService.SignIn(signInBlank, HttpContext);
	}

	[HttpPost("[action]")]
	public async Task<IActionResult> SignUp(SignUpBlank signUpBlank)
	{
		return await _authService.SignUp(signUpBlank, HttpContext);
	}

	[HttpPost("[action]")]
	public new async Task<IActionResult> SignOut()
	{
		return await _authService.SignOut(HttpContext);
	}

	[HttpPost("[action]")]
	[Authorize]
	public bool Signed()
	{
		return true;
	}
}