using StarWarsProgressBarIssueTracker.Domain;
using StarWarsProgressBarIssueTracker.Domain.Issues;

namespace StarWarsProgressBarIssueTracker.Infrastructure.Repositories;

public interface IIssueRepository : IRepository<Issue>;
