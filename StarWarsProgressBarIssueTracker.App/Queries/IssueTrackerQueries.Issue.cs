using StarWarsProgressBarIssueTracker.Domain.Issues;

namespace StarWarsProgressBarIssueTracker.App.Queries;

public partial class IssueTrackerQueries
{
    public async Task<IEnumerable<Issue>> GetIssues()
    {
        return await issueService.GetAllIssues();
    }

    public async Task<Issue?> GetIssue(Guid id)
    {
        return await issueService.GetIssue(id);
    }
}
