using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Luna.Tools.Auth.Options;

public class JwtOptions(IConfiguration configuration)
{
	private String Key => configuration["JWT:Key"] ?? throw new ArgumentNullException("Key");

	public SymmetricSecurityKey SymmetricSecurityKey => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));

	public String Issuer => configuration["JWT:Issuer"] ?? throw new ArgumentNullException("Issuer");

	public String Audience => configuration["JWT:Audience"] ?? throw new ArgumentNullException("Audience");
}