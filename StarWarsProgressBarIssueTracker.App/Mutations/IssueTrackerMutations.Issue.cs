using StarWarsProgressBarIssueTracker.Domain.Issues;
using StarWarsProgressBarIssueTracker.Domain.Milestones;
using StarWarsProgressBarIssueTracker.Domain.Releases;

namespace StarWarsProgressBarIssueTracker.App.Mutations;

public partial class IssueTrackerMutations
{
    public async Task<Issue> AddIssue(string title, string? description, Priority priority, IssueType issueType,
        Guid? milestoneId, Guid? releaseId, Vehicle? vehicle)
    {
        Milestone? milestone = null;
        if (milestoneId is not null)
        {
            milestone = new Milestone { Id = milestoneId.Value, Title = string.Empty };
        }
        Release? release = null;
        if (releaseId is not null)
        {
            release = new Release { Id = releaseId.Value, Title = string.Empty };
        }

        return await issueService.AddIssue(new()
        {
            Title = title,
            Description = description,
            Milestone = milestone,
            IssueState = IssueState.Open,
            Priority = priority,
            IssueType = issueType,
            Release = release,
            Vehicle = vehicle
        });
    }

    public async Task<Issue> UpdateIssue(Guid id, string title, string? description, Priority priority, IssueType issueType,
        Guid? milestoneId, Guid? releaseId, Vehicle? vehicle)
    {
        Milestone? milestone = null;
        if (milestoneId is not null)
        {
            milestone = new Milestone { Id = milestoneId.Value, Title = string.Empty };
        }
        Release? release = null;
        if (releaseId is not null)
        {
            release = new Release { Id = releaseId.Value, Title = string.Empty };
        }

        return await issueService.UpdateIssue(new()
        {
            Id = id,
            Title = title,
            Description = description,
            Milestone = milestone,
            IssueState = IssueState.Open,
            Priority = priority,
            IssueType = issueType,
            Release = release,
            Vehicle = vehicle
        });
    }

    public async Task<Issue> DeleteIssue(Guid id)
    {
        return await issueService.DeleteIssue(new Issue
        {
            Id = id,
            Title = string.Empty
        });
    }
}
