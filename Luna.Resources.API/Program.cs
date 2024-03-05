var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.UseHttpsRedirection();

app.Map("/", async context =>
{
	await context.Response.WriteAsync("available");
});

app.UseStaticFiles();

app.UseCors();

app.UseHttpsRedirection();

app.Run();
