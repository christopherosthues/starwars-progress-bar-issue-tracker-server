using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StarWarsProgressBarIssueTracker.Domain.Milestones;
using StarWarsProgressBarIssueTracker.Infrastructure.Database;
using StarWarsProgressBarIssueTracker.Infrastructure.Models;

namespace StarWarsProgressBarIssueTracker.Infrastructure.Repositories;

public class MilestoneRepository(IssueTrackerContext context, IMapper mapper)
    : IssueTrackerRepositoryBase<Milestone, DbMilestone>(context, mapper), IMilestoneRepository
{
    protected override IQueryable<DbMilestone> GetIncludingFields()
    {
        return DbSet.Include(dbMilestone => dbMilestone.Issues);
    }

    protected override async Task<DbMilestone> Map(Milestone domain, bool add = false, bool update = false)
    {
        if (add)
        {
            return new DbMilestone
            {
                Title = domain.Title,
                Description = domain.Description,
                MilestoneState = domain.MilestoneState
            };
        }

        var dbMilestone = await DbSet.FindAsync(domain.Id);

        if (dbMilestone is null)
        {
            // TODO: throw domain exception
            throw new NullReferenceException($"Not found {domain.Id}");
        }

        if (update)
        {
            dbMilestone.Title = domain.Title;
            dbMilestone.Description = domain.Description;
            dbMilestone.MilestoneState = domain.MilestoneState;
            dbMilestone.LastModifiedAt = DateTime.UtcNow;
        }

        return dbMilestone;
    }

    protected override void DeleteRelationships(DbMilestone entity)
    {
        entity.Issues.Clear();
    }
}
