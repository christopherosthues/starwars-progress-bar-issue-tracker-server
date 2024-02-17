using StarWarsProgressBarIssueTracker.Domain.Issues;
using StarWarsProgressBarIssueTracker.Domain.Labels;
using StarWarsProgressBarIssueTracker.Domain.Milestones;
using StarWarsProgressBarIssueTracker.Domain.Releases;

namespace StarWarsProgressBarIssueTracker.App.Queries;

public partial class IssueTrackerQueries(
    ILabelService labelService,
    IIssueService issueService,
    IMilestoneService milestoneService,
    IReleaseService releaseService);
