using StarWarsProgressBarIssueTracker.App.Appearances;
using StarWarsProgressBarIssueTracker.App.Issues;
using StarWarsProgressBarIssueTracker.App.Mappers;
using StarWarsProgressBarIssueTracker.App.Milestones;
using StarWarsProgressBarIssueTracker.App.Releases;
using StarWarsProgressBarIssueTracker.Domain.Appearances;
using StarWarsProgressBarIssueTracker.Domain.Issues;
using StarWarsProgressBarIssueTracker.Domain.Milestones;
using StarWarsProgressBarIssueTracker.Domain.Releases;
using StarWarsProgressBarIssueTracker.Infrastructure.Repositories;

namespace StarWarsProgressBarIssueTracker.App.ServiceCollectionExtensions;

public static class ServiceRegistrationExtensions
{
    public static void AddIssueTrackerServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IAppearanceRepository, AppearanceRepository>();
        serviceCollection.AddScoped<IAppearanceService, AppearanceService>();
        serviceCollection.AddScoped<IIssueRepository, IssueRepository>();
        serviceCollection.AddScoped<IIssueService, IssueService>();
        serviceCollection.AddScoped<IMilestoneRepository, MilestoneRepository>();
        serviceCollection.AddScoped<IMilestoneService, MilestoneService>();
        serviceCollection.AddScoped<IReleaseRepository, ReleaseRepository>();
        serviceCollection.AddScoped<IReleaseService, ReleaseService>();
    }

    public static void AddIssueTrackerMappers(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddAutoMapper(typeof(EntityMapperProfile));
    }

    public static void AddGraphQLQueries(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<AppearanceQueries>();
        serviceCollection.AddScoped<IssueQueries>();
        serviceCollection.AddScoped<MilestoneQueries>();
        serviceCollection.AddScoped<ReleaseQueries>();
    }

    public static void AddGraphQLMutations(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<AppearanceMutations>();
        serviceCollection.AddScoped<IssueMutations>();
        serviceCollection.AddScoped<MilestoneMutations>();
        serviceCollection.AddScoped<ReleaseMutations>();
    }
}
