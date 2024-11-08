using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Net.Http.Headers;
using TaskBlaster.TaskManagement.Notifications.Services.Interfaces;
using TaskBlaster.TaskManagement.Notifications.Services.Implementations;
using TaskBlaster.TaskManagement.DAL.Interfaces;
using TaskBlaster.TaskManagement.DAL.Implementations;
using Hangfire;
using Hangfire.PostgreSql;
using TaskBlaster.TaskManagement.Notifications.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Configure Hangfire Server
builder.Services.AddHangfireServer();

// Configure PostgreSQL with Entity Framework, sharing the same database as the Task API
builder.Services.AddDbContext<TaskBlaster.TaskManagement.DAL.TaskBlasterDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("TaskManagementDb")));


// Configure Hangfire with a PostgreSQL storage
builder.Services.AddHangfire(config =>
    config.UsePostgreSqlStorage(builder.Configuration.GetConnectionString("HangfireDb")));


    
// Register IMailService and configure HttpClient for MailjetService
builder.Services.AddHttpClient<IMailService, MailjetService>(client =>
{
    client.BaseAddress = new Uri("https://api.mailjet.com/v3.1/");
    // Set up API key and secret here if using Basic Auth for Mailjet
    var apiKey = builder.Configuration["Mailjet:ApiKey"];
    var apiSecret = builder.Configuration["Mailjet:ApiSecret"];
    var authToken = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"{apiKey}:{apiSecret}"));
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authToken);
});

// Register Task-related services
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();

// Configure JWT Bearer authentication for token-based authentication using Auth0
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var auth0Domain = builder.Configuration["Auth0:Domain"];
        var audience = builder.Configuration["Auth0:Audience"];

        options.Authority = auth0Domain;
        options.Audience = audience;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = auth0Domain,
            ValidAudience = audience,
            ValidateIssuer = true,
            ValidateAudience = true
        };
    });

builder.Services.AddAuthorization();

// Configure Swagger to include JWT Bearer authentication
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "TaskBlaster Notifications API", Version = "v1" });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\""
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();


// Enable Hangfire Dashboard with AllowAllAuthorizationFilter for unrestricted access
app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    Authorization = new[] { new AllowAllAuthorizationFilter() }
});

RecurringJob.AddOrUpdate<ReminderService>("SendDueDateReminder", service => service.SendDueDateReminder(), Cron.MinuteInterval(30));


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
