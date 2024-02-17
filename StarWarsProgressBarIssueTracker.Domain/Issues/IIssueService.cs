namespace StarWarsProgressBarIssueTracker.Domain.Issues;

public interface IIssueService
{
    Task<IEnumerable<Issue>> GetAllIssuesAsync(CancellationToken cancellationToken);

    Task<Issue?> GetIssueAsync(Guid id, CancellationToken cancellationToken);

    Task<Issue> AddIssueAsync(Issue issue, CancellationToken cancellationToken);

    Task<Issue> UpdateIssueAsync(Issue issue, CancellationToken cancellationToken);

    Task<Issue> DeleteIssueAsync(Guid id, CancellationToken cancellationToken);
}
