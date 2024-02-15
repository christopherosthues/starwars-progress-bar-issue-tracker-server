using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StarWarsProgressBarIssueTracker.Domain.Exceptions;
using StarWarsProgressBarIssueTracker.Domain.Milestones;
using StarWarsProgressBarIssueTracker.Infrastructure.Database;
using StarWarsProgressBarIssueTracker.Infrastructure.Models;

namespace StarWarsProgressBarIssueTracker.Infrastructure.Repositories;

public class MilestoneRepository(IssueTrackerContext context, IMapper mapper)
    : IssueTrackerRepositoryBase<Milestone, DbMilestone>(context, mapper), IMilestoneRepository
{
    protected override IQueryable<DbMilestone> GetIncludingFields()
    {
        return DbSet.Include(dbMilestone => dbMilestone.Issues)
            .ThenInclude(dbIssue => dbIssue.Vehicle)
            .ThenInclude(dbVehicle => dbVehicle != null ? dbVehicle.Appearances : null)
            .Include(dbMilestone => dbMilestone.Issues)
            .ThenInclude(dbIssue => dbIssue.Release);
    }

    protected override async Task<DbMilestone> Map(Milestone domain, bool add = false, bool update = false)
    {
        if (add)
        {
            return new DbMilestone
            {
                Title = domain.Title,
                Description = domain.Description,
                State = domain.State
            };
        }

        var dbMilestone = await DbSet.FindAsync(domain.Id) ?? throw new DomainIdNotFoundException(nameof(Milestone), domain.Id.ToString());

        if (update)
        {
            dbMilestone.Title = domain.Title;
            dbMilestone.Description = domain.Description;
            dbMilestone.State = domain.State;
            dbMilestone.LastModifiedAt = DateTime.UtcNow;
        }

        return dbMilestone;
    }

    protected override void DeleteRelationships(DbMilestone entity)
    {
        entity.Issues.Clear();
    }
}
