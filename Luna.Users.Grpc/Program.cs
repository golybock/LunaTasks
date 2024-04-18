using Luna.SharedDataAccess.Notification.Services;
using Luna.Users.Grpc.RabbitMQ;
using Luna.Users.Grpc.Services;
using Luna.Users.Repositories.Repositories;
using Luna.Users.Services.Services;
using Npgsql.Extension.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();

builder.Services.AddScoped<IUserService, UserService>();

var connectionString = builder.Configuration.GetConnectionString("luna_users");
var options = new DatabaseOptions() {ConnectionString = connectionString!};

builder.Services.AddSingleton<IDatabaseOptions>(_ => options);

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserRegistrationService, UserRegistrationService>();
builder.Services.AddScoped<INotificationService, NotificationService>();

var app = builder.Build();

app.MapGrpcService<UsersService>();

app.Run();