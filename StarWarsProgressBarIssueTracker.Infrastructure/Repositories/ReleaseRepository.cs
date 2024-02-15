using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StarWarsProgressBarIssueTracker.Domain.Exceptions;
using StarWarsProgressBarIssueTracker.Domain.Releases;
using StarWarsProgressBarIssueTracker.Infrastructure.Database;
using StarWarsProgressBarIssueTracker.Infrastructure.Models;

namespace StarWarsProgressBarIssueTracker.Infrastructure.Repositories;

public class ReleaseRepository(IssueTrackerContext context, IMapper mapper)
    : IssueTrackerRepositoryBase<Release, DbRelease>(context, mapper), IReleaseRepository
{
    protected override IQueryable<DbRelease> GetIncludingFields()
    {
        return DbSet.Include(dbRelease => dbRelease.Issues)
            .ThenInclude(dbIssue => dbIssue.Milestone)
            .Include(dbRelease => dbRelease.Issues)
            .ThenInclude(dbIssue => dbIssue.Vehicle)
            .ThenInclude(dbVehicle => dbVehicle != null ? dbVehicle.Appearances : null);
    }

    protected override async Task<DbRelease> Map(Release domain, bool add = false, bool update = false)
    {
        if (add)
        {
            return new DbRelease
            {
                Title = domain.Title,
                Notes = domain.Notes,
                Date = domain.Date,
                State = domain.State
            };
        }

        var dbRelease = await DbSet.FindAsync(domain.Id) ?? throw new DomainIdNotFoundException(nameof(Release), domain.Id.ToString());

        if (update)
        {
            dbRelease.Title = domain.Title;
            dbRelease.Notes = domain.Notes;
            dbRelease.Date = domain.Date;
            dbRelease.State = domain.State;
            dbRelease.LastModifiedAt = DateTime.UtcNow;
        }

        return dbRelease;
    }

    protected override void DeleteRelationships(DbRelease entity)
    {
        entity.Issues.Clear();
    }
}
