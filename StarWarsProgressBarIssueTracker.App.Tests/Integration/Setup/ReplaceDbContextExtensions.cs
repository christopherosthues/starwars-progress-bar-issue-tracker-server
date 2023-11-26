using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StarWarsProgressBarIssueTracker.Infrastructure;
using StarWarsProgressBarIssueTracker.Infrastructure.Database;

namespace StarWarsProgressBarIssueTracker.App.Tests.Integration.Setup;

public static class ReplaceDbContextExtensions
{
    public static void ReplaceDbContext(this IServiceCollection services)
    {
        var dbContextDescriptor =
            services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<IssueTrackerContext>));
        if (dbContextDescriptor is not null)
        {
            services.Remove(dbContextDescriptor);
        }

        // Create open SqliteConnection so EF won't automatically close it.
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();

        services.AddDbContext<IssueTrackerContext>((container, options) =>
        {
            options.UseSqlite(connection);
        });
    }
}
