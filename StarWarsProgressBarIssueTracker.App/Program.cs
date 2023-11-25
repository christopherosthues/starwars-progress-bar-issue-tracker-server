using Microsoft.EntityFrameworkCore;
using StarWarsProgressBarIssueTracker.App.Queries;
using StarWarsProgressBarIssueTracker.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// builder.Services.AddGitlabClient();

var connectionString = builder.Configuration.GetConnectionString("IssueTrackerContext");
builder.Services.AddDbContext<IssueTrackerContext>(optionsBuilder => optionsBuilder.UseNpgsql(connectionString));

builder.Services.AddGraphQLServer()
    .AddQueryType<LabelQueries>();

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

    context.Database.Migrate();
}

app.UseHttpsRedirection();

app.MapGraphQL();

app.MapControllers();

app.Run();
