using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using StarWarsProgressBarIssueTracker.App.Tests.Integration.Setup;
using StarWarsProgressBarIssueTracker.Infrastructure.Database;

namespace StarWarsProgressBarIssueTracker.App.Tests.Integration;

/// <summary>
/// Base class for all integration tests.
/// </summary>
public class IntegrationTestBase
{
    protected IssueTrackerWebApplicationFactory ApiFactory = default!;
    protected HttpClient HttpClient = default!;

    [OneTimeSetUp]
    public void SetUpOnceBase()
    {
        ApiFactory = new IssueTrackerWebApplicationFactory();
        HttpClient = ApiFactory.CreateClient(new WebApplicationFactoryClientOptions { AllowAutoRedirect = false });
    }

    [SetUp]
    public void SetUpBase()
    {
        using var scope = ApiFactory.GetServiceProvider().CreateScope();
        var serviceProvider = scope.ServiceProvider;
        var dbContext = serviceProvider.GetRequiredService<IssueTrackerContext>();
        SeedDatabase(dbContext);
    }

    /// <summary>
    /// This method seeds the database before each test. This is the place where you can provide test specific data
    /// with which the database is seeded. The default implementation seeds the database with some mocked data.
    /// </summary>
    /// <param name="dbContext">The database context used to seed the database</param>
    protected virtual void SeedDatabase(IssueTrackerContext dbContext)
    {
        // TODO: seed db with data
    }

    [TearDown]
    public void TearDownBase()
    {
        using var scope = ApiFactory.GetServiceProvider().CreateScope();
        var serviceProvider = scope.ServiceProvider;
        var dbContext = serviceProvider.GetRequiredService<IssueTrackerContext>();
        ResetDatabase(dbContext);
    }

    /// <summary>
    /// This method reset the database after each test. This is the place where you can clear the database. The default
    /// implementation deletes all data from the database.
    /// </summary>
    /// <param name="dbContext">The database context used to reset the database</param>
    protected virtual void ResetDatabase(IssueTrackerContext dbContext)
    {
        dbContext.Issues.RemoveRange(dbContext.Issues);
        dbContext.Milestones.RemoveRange(dbContext.Milestones);
        dbContext.Appearances.RemoveRange(dbContext.Appearances);
        dbContext.Releases.RemoveRange(dbContext.Releases);
    }

    [OneTimeTearDown]
    public async Task TearDownOnceBase()
    {
        HttpClient.Dispose();
        await ApiFactory.DisposeAsync();
    }
}
