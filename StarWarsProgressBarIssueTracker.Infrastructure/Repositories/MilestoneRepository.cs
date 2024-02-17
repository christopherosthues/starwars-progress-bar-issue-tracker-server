using Microsoft.EntityFrameworkCore;
using StarWarsProgressBarIssueTracker.Infrastructure.Models;

namespace StarWarsProgressBarIssueTracker.Infrastructure.Repositories;

public class MilestoneRepository : IssueTrackerRepositoryBase<DbMilestone>
{
    protected override IQueryable<DbMilestone> GetIncludingFields()
    {
        return DbSet.Include(dbMilestone => dbMilestone.Issues)
            .ThenInclude(dbIssue => dbIssue.Vehicle)
            .ThenInclude(dbVehicle => dbVehicle != null ? dbVehicle.Appearances : null)
            .Include(dbMilestone => dbMilestone.Issues)
            .ThenInclude(dbIssue => dbIssue.Release);
    }
}
