using Microsoft.EntityFrameworkCore;
using StarWarsProgressBarIssueTracker.App.Appearances;
using StarWarsProgressBarIssueTracker.App.ServiceCollectionExtensions;
using StarWarsProgressBarIssueTracker.Infrastructure.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// builder.Services.AddGitlabClient();

var connectionString = builder.Configuration.GetConnectionString("IssueTrackerContext");
builder.Services.AddDbContext<IssueTrackerContext>(optionsBuilder => optionsBuilder.UseNpgsql(connectionString));

builder.Services.AddGraphQLServer()
    .AddMutationConventions()
    .AddQueryType<AppearanceQueries>()
    .AddMutationType<AppearanceMutations>();

builder.Services.AddIssueTrackerMappers();
builder.Services.AddIssueTrackerServices();
builder.Services.AddGraphQLQueries();
builder.Services.AddGraphQLMutations();

// builder.Services.AddCors(corsOptions =>
//     corsOptions.AddDefaultPolicy(corsPolicyBuilder =>
//         corsPolicyBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<IssueTrackerContext>();

    await context.Database.MigrateAsync();
}

// app.UseCors();

app.UseHttpsRedirection();

app.MapGraphQL();

// app.MapControllers();

app.Run();

/// <summary>
/// Used for integration tests. Entry point class has to accessible from the custom WebApplicationFactory.
/// </summary>
public partial class Program;
