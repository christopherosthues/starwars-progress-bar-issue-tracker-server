using StarWarsProgressBarIssueTracker.App.Appearances;
using StarWarsProgressBarIssueTracker.App.Mappers;
using StarWarsProgressBarIssueTracker.Domain.Appearances;
using StarWarsProgressBarIssueTracker.Infrastructure.Repositories;

namespace StarWarsProgressBarIssueTracker.App.ServiceCollectionExtensions;

public static class ServiceRegistrationExtensions
{
    public static void AddIssueTrackerServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IAppearanceRepository, AppearanceRepository>();
        // serviceCollection.AddScoped<IRepository<DbIssue>, IssueTrackerRepository<DbIssue>>();
        // serviceCollection.AddScoped<IRepository<DbMilestone>, IssueTrackerRepository<DbMilestone>>();
        // serviceCollection.AddScoped<IRepository<DbRelease>, IssueTrackerRepository<DbRelease>>();

        serviceCollection.AddScoped<IAppearanceService, AppearanceService>();
        // serviceCollection.AddScoped<IssueService>();
        // serviceCollection.AddScoped<MilestoneService>();
        // serviceCollection.AddScoped<ReleaseService>();
    }

    public static void AddIssueTrackerMappers(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddAutoMapper(typeof(EntityMapperProfile));
    }

    public static void AddGraphQLQueries(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<AppearanceQueries>();
    }

    public static void AddGraphQLMutations(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<AppearanceMutations>();
    }
}
