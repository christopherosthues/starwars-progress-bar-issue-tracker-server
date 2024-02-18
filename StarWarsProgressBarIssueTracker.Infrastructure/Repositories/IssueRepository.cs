using Microsoft.EntityFrameworkCore;
using StarWarsProgressBarIssueTracker.Infrastructure.Models;

namespace StarWarsProgressBarIssueTracker.Infrastructure.Repositories;

public class IssueRepository : IssueTrackerRepositoryBase<DbIssue>
{
    protected override IQueryable<DbIssue> GetIncludingFields()
    {
        return Context.Issues.Include(dbIssue => dbIssue.Milestone)
            .Include(dbIssue => dbIssue.Release)
            .Include(dbIssue => dbIssue.Labels)
            .Include(dbIssue => dbIssue.Vehicle)
            .ThenInclude(dbVehicle => dbVehicle != null ? dbVehicle.Appearances : null)
            .Include(dbIssue => dbIssue.Vehicle)
            .ThenInclude(dbVehicle => dbVehicle != null ? dbVehicle.Photos : null)
            .Include(dbIssue => dbIssue.Vehicle)
            .ThenInclude(dbVehicle => dbVehicle != null ? dbVehicle.Translations : null);
    }
}
