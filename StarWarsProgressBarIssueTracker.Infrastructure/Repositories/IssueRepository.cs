using AutoMapper;
using StarWarsProgressBarIssueTracker.Domain.Issues;
using StarWarsProgressBarIssueTracker.Infrastructure.Database;
using StarWarsProgressBarIssueTracker.Infrastructure.Models;

namespace StarWarsProgressBarIssueTracker.Infrastructure.Repositories;

public class IssueRepository(IssueTrackerContext context, IMapper mapper)
    : IssueTrackerRepositoryBase<Issue, DbIssue>(context, mapper), IIssueRepository
{
    protected override Task<DbIssue> Map(Issue domain, bool add = false, bool update = false)
    {
        throw new NotImplementedException();
    }
}
