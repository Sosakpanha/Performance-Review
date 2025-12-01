using TeamGoalTracker.Api.Data;
using TeamGoalTracker.Api.Repositories;
using TeamGoalTracker.Api.Repositories.Interfaces;
using TeamGoalTracker.Api.Services;
using TeamGoalTracker.Api.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Add CORS for frontend development
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Register DatabaseInitializer
builder.Services.AddSingleton<DatabaseInitializer>();

// Register repositories
builder.Services.AddScoped<IMemberRepository, MemberRepository>();
builder.Services.AddScoped<IGoalRepository, GoalRepository>();

// Register services
builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.AddScoped<IGoalService, GoalService>();
builder.Services.AddScoped<IStatsService, StatsService>();

var app = builder.Build();

// Initialize database
var dbInitializer = app.Services.GetRequiredService<DatabaseInitializer>();
dbInitializer.Initialize();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors();
app.MapControllers();

app.Run();
