using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StarWarsProgressBarIssueTracker.Domain.Exceptions;
using StarWarsProgressBarIssueTracker.Infrastructure.Database;
using StarWarsProgressBarIssueTracker.Infrastructure.Models;

namespace StarWarsProgressBarIssueTracker.Infrastructure.Repositories;

public class TaskRepository(IssueTrackerContext context, IMapper mapper) : IssueTrackerRepositoryBase<DbTask, DbTask>(context, mapper), ITaskRepository
{
    protected override IQueryable<DbTask> GetIncludingFields()
    {
        return DbSet.Include(task => task.Job);
    }

    protected override async Task<DbTask> Map(DbTask domain, bool add = false, bool update = false)
    {
        var job = await Context.Jobs.SingleOrDefaultAsync(job => job.Id == domain.Job.Id) ?? throw new DomainIdNotFoundException(nameof(DbJob), domain.Job.Id.ToString());

        if (add)
        {
            return new DbTask
            {
                ExecuteAt = domain.ExecuteAt,
                Status = domain.Status,
                Job = job
            };
        }

        var dbTask = await DbSet.FindAsync(domain.Id) ?? throw new DomainIdNotFoundException(nameof(DbTask), domain.Id.ToString());

        if (update)
        {
            dbTask.Job = job;
            dbTask.Status = domain.Status;
            dbTask.ExecuteAt = domain.ExecuteAt;
            dbTask.ExecutedAt = domain.ExecutedAt;
            dbTask.LastModifiedAt = DateTime.UtcNow;
        }

        return dbTask;
    }

    protected override void DeleteRelationships(DbTask entity)
    {
    }
}
