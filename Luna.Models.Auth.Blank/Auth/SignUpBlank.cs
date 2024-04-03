using System.ComponentModel.DataAnnotations;

namespace Luna.Models.Auth.Blank.Auth;

public class SignUpBlank
{
	[Required]
	public String Email { get; set; } = null!;

	[Required]
	public String Password { get; set; } = null!;

	[Required]
	public String Username { get; set; } = null!;
}