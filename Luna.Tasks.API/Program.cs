using Luna.SharedDataAccess.Users.Services;
using Luna.Tasks.Repositories.Repositories.Card;
using Luna.Tasks.Repositories.Repositories.CardAttributes.Comment;
using Luna.Tasks.Repositories.Repositories.CardAttributes.Role;
using Luna.Tasks.Repositories.Repositories.CardAttributes.Status;
using Luna.Tasks.Repositories.Repositories.CardAttributes.Tag;
using Luna.Tasks.Repositories.Repositories.CardAttributes.Type;
using Luna.Tasks.Repositories.Repositories.Page;
using Luna.Tasks.Services.Services.Card;
using Luna.Tasks.Services.Services.CardAttributes.Comment;
using Luna.Tasks.Services.Services.CardAttributes.Role;
using Luna.Tasks.Services.Services.CardAttributes.Status;
using Luna.Tasks.Services.Services.CardAttributes.Tag;
using Luna.Tasks.Services.Services.CardAttributes.Type;
using Luna.Tasks.Services.Services.Page;
using Luna.Tools.Auth.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Npgsql.Extension.Options;

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
builder.Services.AddSwaggerGen(s =>
{
	s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
		Name = "Authorization",
		In = ParameterLocation.Header,
		Type = SecuritySchemeType.ApiKey,
		Scheme = "Bearer"
	});

	s.AddSecurityRequirement(new OpenApiSecurityRequirement()
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id = "Bearer"
				},
				Scheme = "oauth2",
				Name = "Bearer",
				In = ParameterLocation.Header,

			},
			new List<string>()
		}
	});
});

builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(policy =>
	{
		policy.AllowAnyHeader()
			.AllowAnyMethod()
			.AllowAnyOrigin();
	});
});

// db config
var connectionString = builder.Configuration.GetConnectionString("luna_tasks");
var options = new DatabaseOptions() {ConnectionString = connectionString};

builder.Services.AddSingleton<IDatabaseOptions>(_ => options);

// grpc
builder.Services.AddScoped<IUserService, UserService>();

// db
builder.Services.AddScoped<ICardRepository, CardRepository>();
builder.Services.AddScoped<IPageRepository, PageRepository>();
builder.Services.AddScoped<ITypeRepository, TypeRepository>();
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<IStatusRepository, StatusRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();

// services
builder.Services.AddScoped<ICardService, CardService>();
builder.Services.AddScoped<IPageService, PageService>();
builder.Services.AddScoped<ITypeService, TypeService>();
builder.Services.AddScoped<ITagService, TagService>();
builder.Services.AddScoped<IStatusService, StatusService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<ICommentService, CommentService>();

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
