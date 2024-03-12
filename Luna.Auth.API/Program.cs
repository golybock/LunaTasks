using System.Text;
using Luna.Auth.Repositories.Repositories;
using Luna.Auth.Services.Options;
using Luna.Auth.Services.Services;
using Luna.SharedDataAccess.Users.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Npgsql.Extension.Options;

var builder = WebApplication.CreateBuilder(args);

var jwtOptions = new JwtOptions(builder.Configuration);

builder.Services.AddAuthorization();
builder.Services
	.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(o =>
	{
		o.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuer = true,
			ValidIssuer = jwtOptions.Issuer,
			ValidateAudience = true,
			ValidAudience = jwtOptions.Audience,
			ValidateLifetime = true,
			IssuerSigningKey = jwtOptions.SymmetricSecurityKey,
			ValidateIssuerSigningKey = true
		};
	});


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(policy =>
	{
		policy.AllowAnyHeader()
			.AllowAnyMethod()
			.AllowAnyOrigin();
	});
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	.AddCookie(options =>
	{
		options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
		options.SlidingExpiration = true;
	});

var connectionString = builder.Configuration.GetConnectionString("luna_auth");
var databaseOptions = new DatabaseOptions() {ConnectionString = connectionString!};

builder.Services.AddSingleton<IDatabaseOptions>(_ => databaseOptions);

builder.Services.AddScoped<JwtOptions>();

builder.Services.AddScoped<IUserAuthRepository, UserAuthRepository>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();