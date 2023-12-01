using StarWarsProgressBarIssueTracker.Domain.Issues;
using StarWarsProgressBarIssueTracker.Infrastructure.Repositories;

namespace StarWarsProgressBarIssueTracker.App.Issues;

public class IssueService(IIssueRepository repository) : IIssueService
{
    public Task<IEnumerable<Issue>> GetAllIssues()
    {
        return repository.GetAll();
    }

    public Task<Issue?> GetIssue(Guid id)
    {
        return repository.GetById(id);
    }

    public Task<Issue> AddIssue(Issue issue)
    {
        return repository.Add(issue);
    }

    public Task<Issue> UpdateIssue(Issue issue)
    {
        return repository.Update(issue);
    }

    public Task<Issue> DeleteIssue(Issue issue)
    {
        return repository.Delete(issue);
    }
}
