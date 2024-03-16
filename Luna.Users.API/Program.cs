using Luna.Auth.Services.Options;
using Luna.SharedDataAccess.Users.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

var jwtOptions = new JwtOptions(builder.Configuration);

builder.Services.AddAuthentication(opt =>
	{
		opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
		opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	})
	.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, o =>
	{
		o.Authority = jwtOptions.Issuer;
		o.Audience = jwtOptions.Audience;
		o.RequireHttpsMetadata = false;
		o.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuer = true,
			ValidIssuer = jwtOptions.Issuer,
			ValidateAudience = false,
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