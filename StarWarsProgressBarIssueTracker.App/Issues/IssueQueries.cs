using StarWarsProgressBarIssueTracker.Domain.Issues;

namespace StarWarsProgressBarIssueTracker.App.Issues;

public class IssueQueries(IIssueService issueService)
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
