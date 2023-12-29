using AutoMapper;
using StarWarsProgressBarIssueTracker.Domain.Exceptions;
using StarWarsProgressBarIssueTracker.Infrastructure.Database;
using StarWarsProgressBarIssueTracker.Infrastructure.Models;

namespace StarWarsProgressBarIssueTracker.Infrastructure.Repositories;

public class JobRepository(IssueTrackerContext context, IMapper mapper) : IssueTrackerRepositoryBase<DbJob, DbJob>(context, mapper), IJobRepository
{
    protected override void DeleteRelationships(DbJob entity)
    {
    }

    protected override async Task<DbJob> Map(DbJob domain, bool add = false, bool update = false)
    {
        if (add)
        {
            return new DbJob
            {
                CronInterval = domain.CronInterval,
                NextExecution = domain.NextExecution,
                IsPaused = domain.IsPaused,
            };
        }

        var dbJob = await DbSet.FindAsync(domain.Id) ?? throw new DomainIdNotFoundException(nameof(DbJob), domain.Id.ToString());

        if (update)
        {
            dbJob.NextExecution = domain.NextExecution;
            dbJob.CronInterval = domain.CronInterval;
            dbJob.IsPaused = domain.IsPaused;
            dbJob.LastModifiedAt = DateTime.UtcNow;
        }

        return dbJob;
    }
}
