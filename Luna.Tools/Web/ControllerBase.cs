using System.Security.Claims;

namespace Luna.Tools.Web;

public class ControllerBase : Microsoft.AspNetCore.Mvc.ControllerBase
{
	protected string? UserEmail =>
		User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

	protected Guid UserId =>
		Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Authentication)?.Value);
}