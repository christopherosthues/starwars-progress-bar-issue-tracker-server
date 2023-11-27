using StarWarsProgressBarIssueTracker.App.Appearances;
using StarWarsProgressBarIssueTracker.App.Milestones;
using StarWarsProgressBarIssueTracker.App.Releases;
using StarWarsProgressBarIssueTracker.Domain;
using StarWarsProgressBarIssueTracker.Domain.Appearances;
using StarWarsProgressBarIssueTracker.Domain.Issues;
using StarWarsProgressBarIssueTracker.Domain.Milestones;
using StarWarsProgressBarIssueTracker.Domain.Releases;
using StarWarsProgressBarIssueTracker.Infrastructure;
using StarWarsProgressBarIssueTracker.Infrastructure.Models;

namespace StarWarsProgressBarIssueTracker.App.ServiceCollectionExtensions;

public static class ServiceRegistrationExtensions
{
    public static void AddIssueTrackerServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IAppearanceService, AppearanceService>();
        serviceCollection.AddTransient<IIssueService, IIssueService>();
        serviceCollection.AddTransient<IMilestoneService, MilestoneService>();
        serviceCollection.AddTransient<IReleaseService, ReleaseService>();

        serviceCollection.AddTransient<IRepository<DbAppearance>, IssueTrackerRepository<DbAppearance>>();
        serviceCollection.AddTransient<IRepository<DbIssue>, IssueTrackerRepository<DbIssue>>();
        serviceCollection.AddTransient<IRepository<DbMilestone>, IssueTrackerRepository<DbMilestone>>();
        serviceCollection.AddTransient<IRepository<DbRelease>, IssueTrackerRepository<DbRelease>>();
    }
}
