using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

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

app.Map("/", async context =>
{
	await context.Response.WriteAsync("available");
});

app.UseCors();

app.UseHttpsRedirection();

app.UseRouting();
app.UseOcelot().Wait();

app.Run();