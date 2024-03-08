using System.ComponentModel.DataAnnotations;

namespace Luna.Models.Auth.Blank.Auth;

public class SignInBlank
{
	[Required]
	public String Email { get; set; } = null!;

	[Required]
	public String Password { get; set; } = null!;
}