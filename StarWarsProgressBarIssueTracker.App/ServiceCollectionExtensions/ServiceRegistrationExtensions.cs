using StarWarsProgressBarIssueTracker.App.Appearances;
using StarWarsProgressBarIssueTracker.App.Issues;
using StarWarsProgressBarIssueTracker.App.Jobs;
using StarWarsProgressBarIssueTracker.App.Mappers;
using StarWarsProgressBarIssueTracker.App.Milestones;
using StarWarsProgressBarIssueTracker.App.Mutations;
using StarWarsProgressBarIssueTracker.App.Queries;
using StarWarsProgressBarIssueTracker.App.Releases;
using StarWarsProgressBarIssueTracker.Domain.Appearances;
using StarWarsProgressBarIssueTracker.Domain.Issues;
using StarWarsProgressBarIssueTracker.Domain.Milestones;
using StarWarsProgressBarIssueTracker.Domain.Releases;
using StarWarsProgressBarIssueTracker.Infrastructure.Database;
using StarWarsProgressBarIssueTracker.Infrastructure.Repositories;

namespace StarWarsProgressBarIssueTracker.App.ServiceCollectionExtensions;

public static class ServiceRegistrationExtensions
{
    public static void AddIssueTrackerConfigurations(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.Configure<IssueTrackerDbConfig>(configuration.GetSection("IssueTrackerDbConfig"));
    }

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
        serviceCollection.AddScoped<ITaskRepository, TaskRepository>();
        serviceCollection.AddScoped<IJobRepository, JobRepository>();

        serviceCollection.AddScoped<JobSchedulingService>();
        serviceCollection.AddScoped<JobExecutionService>();
        serviceCollection.AddScoped<JobFactory>();
    }

    public static void AddIssueTrackerMappers(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddAutoMapper(typeof(EntityMapperProfile));
    }

    public static void AddGraphQLQueries(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IssueTrackerQueries>();
    }

    public static void AddGraphQLMutations(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IssueTrackerMutations>();
    }

    public static void AddJobs(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<JobScheduler>();
        serviceCollection.AddScoped<GitHubSynchronizationJobScheduler>();
        serviceCollection.AddScoped<GitlabSynchronizationJobScheduler>();
        serviceCollection.AddScoped<GitHubSynchronizationJob>();
        serviceCollection.AddScoped<GitlabSynchronizationJob>();
    }
}
