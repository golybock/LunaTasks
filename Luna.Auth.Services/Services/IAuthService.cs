using Luna.Models.Auth.Blank.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Luna.Auth.Services.Services;

public interface IAuthService
{
	public Task<IActionResult> SignIn(SignInBlank signInBlank, HttpContext context);

	public Task<IActionResult> SignUp(SignUpBlank signUpBlank, HttpContext context);

	public Task<IActionResult> SignOut(HttpContext context);
}