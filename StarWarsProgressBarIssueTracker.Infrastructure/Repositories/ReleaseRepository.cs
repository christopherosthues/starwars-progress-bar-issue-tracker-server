using AutoMapper;
using StarWarsProgressBarIssueTracker.Domain.Releases;
using StarWarsProgressBarIssueTracker.Infrastructure.Database;
using StarWarsProgressBarIssueTracker.Infrastructure.Models;

namespace StarWarsProgressBarIssueTracker.Infrastructure.Repositories;

public class ReleaseRepository(IssueTrackerContext context, IMapper mapper)
    : IssueTrackerRepositoryBase<Release, DbRelease>(context, mapper), IReleaseRepository
{
    protected override async Task<DbRelease> Map(Release domain, bool add = false, bool update = false)
    {
        if (add)
        {
            return new DbRelease
            {
                Title = domain.Title,
                ReleaseNotes = domain.ReleaseNotes,
                ReleaseDate = domain.ReleaseDate,
                ReleaseState = domain.ReleaseState
            };
        }

        var dbRelease = await DbSet.FindAsync(domain.Id);

        if (dbRelease is null)
        {
            // TODO: throw domain exception
            throw new NullReferenceException($"Not found {domain.Id}");
        }

        if (update)
        {
            dbRelease.Title = domain.Title;
            dbRelease.ReleaseNotes = domain.ReleaseNotes;
            dbRelease.ReleaseDate = domain.ReleaseDate;
            dbRelease.ReleaseState = domain.ReleaseState;
            dbRelease.LastModifiedAt = DateTime.UtcNow;
        }

        return dbRelease;
    }
}
