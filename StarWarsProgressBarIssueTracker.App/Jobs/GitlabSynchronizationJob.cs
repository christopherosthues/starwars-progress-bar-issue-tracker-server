
using StarWarsProgressBarIssueTracker.Domain.Issues;
using StarWarsProgressBarIssueTracker.Domain.Labels;
using StarWarsProgressBarIssueTracker.Domain.Milestones;
using StarWarsProgressBarIssueTracker.Domain.Releases;
using StarWarsProgressBarIssueTracker.Infrastructure.Gitlab.GraphQL;
using StarWarsProgressBarIssueTracker.Infrastructure.Gitlab.Networking;
using IssueState = StarWarsProgressBarIssueTracker.Infrastructure.Gitlab.GraphQL.IssueState;

namespace StarWarsProgressBarIssueTracker.App.Jobs;

public class GitlabSynchronizationJob(GraphQLService graphQlService,
    // RestService restService
    ILabelService labelService,
    IMilestoneService milestoneService,
    // IIssueService issueService,
    IReleaseService releaseService
    )
    : IJob
{
    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        var all = await graphQlService.GetAllAsync(cancellationToken);

        await SynchronizeLabelsAsync(all?.Labels, cancellationToken);

        await SynchronizeMilestonesAsync(all?.Milestones, cancellationToken);

        await SynchronizeIssuesAsync(all?.Issues, cancellationToken);
    }

    private async Task SynchronizeLabelsAsync(IGetAll_Project_Labels? gitlabLabelData, CancellationToken cancellationToken)
    {
        IEnumerable<IGetAll_Project_Labels_Nodes> gitlabLabels = (gitlabLabelData?.Nodes ?? [])
            .Where(gitlabLabel => gitlabLabel != null).Cast<IGetAll_Project_Labels_Nodes>();
        IList<Label> labels = gitlabLabels.Select(gitlabLabel =>
            new Label
            {
                GitlabId = gitlabLabel.Id,
                Title = gitlabLabel.Title,
                Description = gitlabLabel.Description,
                Color = gitlabLabel.Color,
                TextColor = gitlabLabel.TextColor,
                LastModifiedAt = DateTime.Parse(gitlabLabel.UpdatedAt).ToUniversalTime()
            }).ToList();

        await labelService.SynchronizeFromGitlabAsync(labels, cancellationToken);
    }

    private async Task SynchronizeMilestonesAsync(IGetAll_Project_Milestones? gitlabMilestoneData, CancellationToken cancellationToken)
    {
        IEnumerable<IGetAll_Project_Milestones_Nodes> gitlabMilestones = (gitlabMilestoneData?.Nodes ?? [])
            .Where(gitlabMilestone => gitlabMilestone != null).Cast<IGetAll_Project_Milestones_Nodes>();
        IList<Milestone> milestones = gitlabMilestones.Select(gitlabMilestone =>
            new Milestone
            {
                GitlabId = gitlabMilestone.Id,
                GitlabIid = gitlabMilestone.Iid,
                Title = gitlabMilestone.Title,
                Description = gitlabMilestone.Description,
                State = MapMilestoneState(gitlabMilestone.State),
                LastModifiedAt = DateTime.Parse(gitlabMilestone.UpdatedAt).ToUniversalTime()
            }).ToList();

        await milestoneService.SynchronizeFromGitlabAsync(milestones, cancellationToken);
    }

    private static MilestoneState MapMilestoneState(MilestoneStateEnum milestoneState) =>
        milestoneState switch
        {
            MilestoneStateEnum.Active => MilestoneState.Open,
            MilestoneStateEnum.Closed => MilestoneState.Closed,
            _ => MilestoneState.Closed
        };

    private async Task SynchronizeIssuesAsync(IGetAll_Project_Issues? gitlabIssueData, CancellationToken cancellationToken)
    {
        var currentPageInfo = gitlabIssueData?.PageInfo;

        var gitlabFurtherIssues = await LoadAllRemainingIssues(cancellationToken, currentPageInfo);

        IEnumerable<IGetAll_Project_Issues_Nodes> gitlabFirstIssues = (gitlabIssueData?.Nodes ?? []).Where(issue => issue != null).Cast<IGetAll_Project_Issues_Nodes>();
        IList<Issue> issues = [];
        IList<Release> releases = [];
        foreach (var gitlabIssue in gitlabFirstIssues)
        {
            if (gitlabIssue.Title.Contains("Release"))
            {
                releases.Add(new Release
                {
                    Title = gitlabIssue.Title,
                    Notes = gitlabIssue.Description,
                    State = MapReleaseState(gitlabIssue.State),
                    Date = gitlabIssue.DueDate != null ? DateTime.Parse(gitlabIssue.DueDate) : null,
                    LastModifiedAt = DateTime.Parse(gitlabIssue.UpdatedAt).ToUniversalTime()
                });
            }
            else
            {
                issues.Add(new Issue
                {
                    Title = gitlabIssue.Title,
                    Description = gitlabIssue.Description,
                    State = MapIssueState(gitlabIssue.State),
                    LastModifiedAt = DateTime.Parse(gitlabIssue.UpdatedAt).ToUniversalTime()
                });
            }
        }
        foreach (var gitlabIssue in gitlabFurtherIssues)
        {
            if (gitlabIssue.Title.Contains("Release"))
            {
                releases.Add(new Release
                {
                    Title = gitlabIssue.Title,
                    Notes = gitlabIssue.Description,
                    State = MapReleaseState(gitlabIssue.State),
                    Date = gitlabIssue.DueDate != null ? DateTime.Parse(gitlabIssue.DueDate) : null,
                    LastModifiedAt = DateTime.Parse(gitlabIssue.UpdatedAt).ToUniversalTime()
                });
            }
            else
            {
                issues.Add(new Issue
                {
                    Title = gitlabIssue.Title,
                    Description = gitlabIssue.Description,
                    State = MapIssueState(gitlabIssue.State),
                    LastModifiedAt = DateTime.Parse(gitlabIssue.UpdatedAt).ToUniversalTime()
                });
            }
        }

        await releaseService.SynchronizeFromGitlabAsync(releases, cancellationToken);
    }

    private async Task<List<IGetFurtherIssues_Project_Issues_Nodes>> LoadAllRemainingIssues(CancellationToken cancellationToken,
        IGetAll_Project_Issues_PageInfo? currentPageInfo)
    {
        List<IGetFurtherIssues_Project_Issues_Nodes> gitlabFurtherIssues = [];

        if ((currentPageInfo?.HasNextPage ?? false) && !string.IsNullOrEmpty(currentPageInfo.EndCursor))
        {
            var nextIssuesResult = await graphQlService.GetFurtherIssuesAsync(currentPageInfo.EndCursor, cancellationToken);

            gitlabFurtherIssues.AddRange(nextIssuesResult?.Nodes?.Where(issue => issue != null)?.Cast<IGetFurtherIssues_Project_Issues_Nodes>() ?? []);

            while ((nextIssuesResult?.PageInfo?.HasNextPage ?? false) && !string.IsNullOrEmpty(nextIssuesResult.PageInfo.EndCursor))
            {
                nextIssuesResult = await graphQlService.GetFurtherIssuesAsync(currentPageInfo.EndCursor, cancellationToken);
                gitlabFurtherIssues.AddRange(nextIssuesResult?.Nodes?.Where(issue => issue != null)?.Cast<IGetFurtherIssues_Project_Issues_Nodes>() ?? []);
            }
        }

        return gitlabFurtherIssues;
    }

    private static ReleaseState MapReleaseState(IssueState issueState) =>
        issueState switch
        {
            IssueState.Opened => ReleaseState.Planned,
            IssueState.Closed => ReleaseState.Released,
            _ => ReleaseState.Released
        };

    private static Domain.Issues.IssueState MapIssueState(IssueState issueState) =>
        issueState switch
        {
            IssueState.Opened => Domain.Issues.IssueState.Open,
            IssueState.Closed => Domain.Issues.IssueState.Closed,
            _ => Domain.Issues.IssueState.Closed
        };
}
