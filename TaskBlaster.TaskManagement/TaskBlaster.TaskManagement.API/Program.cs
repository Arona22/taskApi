using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TaskBlaster.TaskManagement.API.Services.Implementations;
using TaskBlaster.TaskManagement.API.Services.Interfaces;
using TaskBlaster.TaskManagement.DAL.Interfaces;
using TaskBlaster.TaskManagement.DAL.Implementations;
using TaskBlaster.TaskManagement.Models.InputModels;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
TimeZoneInfo.ClearCachedData();
TimeZoneInfo utc = TimeZoneInfo.Utc;


// Add services to the container
builder.Services.AddControllers();
builder.Services.AddHttpClient();

// Register Application Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<INotificationService, NotificationService>();

// Register Repository Services
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();

// Configure PostgreSQL with Entity Framework
builder.Services.AddDbContext<TaskBlaster.TaskManagement.DAL.TaskBlasterDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("TaskManagementDb")));

// Configure JWT Bearer authentication for token-based authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var auth0Domain = builder.Configuration["Auth0:Domain"];
        var audience = builder.Configuration["Auth0:Audience"];

        options.Authority = $"https://{auth0Domain}/";
        options.Audience = audience;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = $"https://{auth0Domain}/",
            ValidAudience = audience,
            ValidateIssuer = true,
            ValidateAudience = true
        };

        options.Events = new JwtBearerEvents
        {
            OnTokenValidated = async context =>
            {
                var userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
                var claims = context.Principal?.Claims;

                // Retrieve email and name from the token claims
                var userEmail = claims?.FirstOrDefault(c => c.Type == "email")?.Value;
                var userName = claims?.FirstOrDefault(c => c.Type == "name")?.Value;

                if (!string.IsNullOrEmpty(userEmail))
                {
                    // Construct UserInputModel based on token claims
                    var inputModel = new UserInputModel
                    {
                        FullName = userName ?? "Unknown User",
                        EmailAddress = userEmail,
                        ProfileImageUrl = claims?.FirstOrDefault(c => c.Type == "picture")?.Value
                    };

                    // Call CreateUserIfNotExistsAsync with the constructed input model
                    await userService.CreateUserIfNotExistsAsync(inputModel);
                }
            }
        };
    });

// Configure Swagger
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
