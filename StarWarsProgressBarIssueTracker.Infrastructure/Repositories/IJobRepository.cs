using StarWarsProgressBarIssueTracker.Domain;
using StarWarsProgressBarIssueTracker.Infrastructure.Models;

namespace StarWarsProgressBarIssueTracker.Infrastructure.Repositories;

public interface IJobRepository : IRepository<DbJob>
{
}
