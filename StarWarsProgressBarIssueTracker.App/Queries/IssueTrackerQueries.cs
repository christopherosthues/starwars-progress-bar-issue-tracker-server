using StarWarsProgressBarIssueTracker.Domain.Appearances;
using StarWarsProgressBarIssueTracker.Domain.Issues;
using StarWarsProgressBarIssueTracker.Domain.Milestones;
using StarWarsProgressBarIssueTracker.Domain.Releases;

namespace StarWarsProgressBarIssueTracker.App.Queries;

public partial class IssueTrackerQueries(
    IAppearanceService appearanceService,
    IIssueService issueService,
    IMilestoneService milestoneService,
    IReleaseService releaseService);
