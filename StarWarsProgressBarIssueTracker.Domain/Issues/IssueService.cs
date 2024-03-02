using StarWarsProgressBarIssueTracker.Domain.Exceptions;

namespace StarWarsProgressBarIssueTracker.Domain.Issues;

public class IssueService(IDataPort<Issue> dataPort) : IIssueService
{
    public async Task<IEnumerable<Issue>> GetAllIssuesAsync(CancellationToken cancellationToken)
    {
        return await dataPort.GetAllAsync(cancellationToken);
    }

    public async Task<Issue?> GetIssueAsync(Guid id, CancellationToken cancellationToken)
    {
        return await dataPort.GetByIdAsync(id, cancellationToken);
    }

    public async Task<Issue> AddIssueAsync(Issue issue, CancellationToken cancellationToken)
    {
        ValidateIssue(issue);

        return await dataPort.AddAsync(issue, cancellationToken);
    }

    private static void ValidateIssue(Issue issue)
    {
        var errors = new List<Exception>();
        if (string.IsNullOrWhiteSpace(issue.Title))
        {
            errors.Add(new ValueNotSetException(nameof(Issue.Title)));
        }

        if (issue.Title.Length < 1)
        {
            errors.Add(new StringTooShortException(issue.Title, nameof(Issue.Title),
                $"The length of {nameof(Issue.Title)} has to be between {IssueConstants.MinTitleLength} and {IssueConstants.MaxTitleLength}."));
        }

        if (issue.Title.Length > IssueConstants.MaxTitleLength)
        {
            errors.Add(new StringTooLongException(issue.Title, nameof(Issue.Title),
                $"The length of {nameof(Issue.Title)} has to be between {IssueConstants.MinTitleLength} and {IssueConstants.MaxTitleLength}."));
        }

        if (issue.Description is not null && issue.Description.Length > IssueConstants.MaxDescriptionLength)
        {
            errors.Add(new StringTooLongException(issue.Description, nameof(Issue.Description),
                $"The length of {nameof(Issue.Description)} has to be less than {IssueConstants.MaxDescriptionLength + 1}."));
        }

        if (!Enum.IsDefined(issue.State) || issue.State == IssueState.Unknown)
        {
            errors.Add(new ValueNotSetException(nameof(Issue.State)));
        }

        if (!Enum.IsDefined(issue.Priority) || issue.Priority == Priority.Unknown)
        {
            errors.Add(new ValueNotSetException(nameof(Issue.Priority)));
        }

        if (errors.Count != 0)
        {
            throw new AggregateException(errors);
        }
    }

    public async Task<Issue> UpdateIssueAsync(Issue issue, CancellationToken cancellationToken)
    {
        ValidateIssue(issue);

        if (!(await dataPort.ExistsAsync(issue.Id, cancellationToken)))
        {
            throw new DomainIdNotFoundException(nameof(Issue), issue.Id.ToString());
        }

        return await dataPort.UpdateAsync(issue, cancellationToken);
    }

    public async Task<Issue> DeleteIssueAsync(Guid id, CancellationToken cancellationToken)
    {
        if (!(await dataPort.ExistsAsync(id, cancellationToken)))
        {
            throw new DomainIdNotFoundException(nameof(Issue), id.ToString());
        }

        return await dataPort.DeleteAsync(id, cancellationToken);
    }

    public async Task SynchronizeFromGitlabAsync(IList<Issue> issues, CancellationToken cancellationToken)
    {
        var existingIssues = await dataPort.GetAllAsync(cancellationToken);

        var issuesToAdd = issues.Where(issue =>
            !existingIssues.Any(existingIssue => issue.GitlabId!.Equals(existingIssue.GitlabId)));

        var issuesToDelete = existingIssues.Where(existingIssue => existingIssue.GitlabId != null &&
                                                                   !issues.Any(issue => issue.GitlabId!.Equals(existingIssue.GitlabId)));

        await dataPort.AddRangeAsync(issuesToAdd, cancellationToken);

        await dataPort.DeleteRangeAsync(issuesToDelete, cancellationToken);

        // TODO: Update issues, resolve conflicts
    }
}
