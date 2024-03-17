using Luna.Tools.Auth.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

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
	});

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Configuration.AddJsonFile("ocelot.Development.json", optional: false, reloadOnChange: true);

builder.Services.AddOcelot(builder.Configuration);

builder.Services.AddCors(cors =>
{
	cors.AddDefaultPolicy(options =>
	{
		options.AllowAnyMethod()
			.AllowAnyHeader()
			.AllowAnyOrigin();
	});
});

var app = builder.Build();

app.Map("/", async context => { await context.Response.WriteAsync("available"); });

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthentication();

// todo костыль, иначе ocelot не пропускает токены
var configuration = new OcelotPipelineConfiguration
{
	AuthenticationMiddleware = async (cpt, est) =>
	{
		await est.Invoke();
	}
};

app.UseRouting();
app.UseOcelot(configuration).Wait();

app.Run();