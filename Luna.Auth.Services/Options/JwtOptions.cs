using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Luna.Auth.Services.Options;

public class JwtOptions
{
	private IConfiguration _configuration;

	public JwtOptions(IConfiguration configuration)
	{
		_configuration = configuration;
	}

	private String Key => _configuration["JWT:Key"] ?? throw new ArgumentNullException("Key");

	public SymmetricSecurityKey SymmetricSecurityKey => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));

	public String Issuer => _configuration["JWT:Issuer"] ?? throw new ArgumentNullException("Issuer");

	public String Audience => _configuration["JWT:Audience"] ?? throw new ArgumentNullException("Audience");
}