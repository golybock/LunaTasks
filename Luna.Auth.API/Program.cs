using Luna.Auth.Repositories.Repositories;
using Luna.Auth.Services.Services;
using Luna.SharedDataAccess.Users.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Npgsql.Extension.Options;

var builder = WebApplication.CreateBuilder(args);


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
var options = new DatabaseOptions() {ConnectionString = connectionString!};

builder.Services.AddSingleton<IDatabaseOptions>(_ => options);

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

var cookiePolicyOptions = new CookiePolicyOptions
{
	MinimumSameSitePolicy = SameSiteMode.Strict,
};

app.UseCookiePolicy(cookiePolicyOptions);

app.MapControllers();

app.Run();
