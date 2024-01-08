using Microsoft.EntityFrameworkCore;
using StarWarsProgressBarIssueTracker.Infrastructure.Database;

namespace StarWarsProgressBarIssueTracker.App.Extensions;

public static class DbContextExtensions
{
    public static void RegisterDbContext(this IServiceCollection services, string? connectionString)
    {
        services.AddDbContext<IssueTrackerContext>(optionsBuilder => optionsBuilder.UseNpgsql(connectionString));
    }

    public static async Task EnsureDbUpdatedAsync(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var services = scope.ServiceProvider;

        var context = services.GetRequiredService<IssueTrackerContext>();

        await context.Database.MigrateAsync();
    }
}
