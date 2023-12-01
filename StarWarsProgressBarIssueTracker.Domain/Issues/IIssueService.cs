namespace StarWarsProgressBarIssueTracker.Domain.Issues;

public interface IIssueService
{
    public Task<IEnumerable<Issue>> GetAllIssues();

    public Task<Issue?> GetIssue(Guid id);

    public Task<Issue> AddIssue(Issue issue);

    public Task<Issue> UpdateIssue(Issue issue);

    public Task<Issue> DeleteIssue(Issue issue);
}
