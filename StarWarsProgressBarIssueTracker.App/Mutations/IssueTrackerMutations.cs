using StarWarsProgressBarIssueTracker.Domain.Appearances;
using StarWarsProgressBarIssueTracker.Domain.Issues;
using StarWarsProgressBarIssueTracker.Domain.Milestones;
using StarWarsProgressBarIssueTracker.Domain.Releases;

namespace StarWarsProgressBarIssueTracker.App.Mutations;

public partial class IssueTrackerMutations(
    IAppearanceService appearanceService,
    IIssueService issueService,
    IMilestoneService milestoneService,
    IReleaseService releaseService);
