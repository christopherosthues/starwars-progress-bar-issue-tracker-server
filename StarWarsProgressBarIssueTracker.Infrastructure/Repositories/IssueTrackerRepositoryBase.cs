using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StarWarsProgressBarIssueTracker.Domain;
using StarWarsProgressBarIssueTracker.Infrastructure.Database;
using StarWarsProgressBarIssueTracker.Infrastructure.Models;

namespace StarWarsProgressBarIssueTracker.Infrastructure.Repositories;

public abstract class IssueTrackerRepositoryBase<TDbEntity>(IssueTrackerContext context, IMapper mapper)
    : IRepository<TDbEntity>
    where TDbEntity : DbEntityBase
{
    protected readonly DbSet<TDbEntity> DbSet = context.Set<TDbEntity>();
    protected readonly IssueTrackerContext Context = context;

    public async Task<TDbEntity?> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        return await GetIncludingFields().FirstOrDefaultAsync(dbEntity => dbEntity.Id.Equals(id), cancellationToken);
    }

    public async Task<IEnumerable<TDbEntity>> GetAll(CancellationToken cancellationToken = default)
    {
        return await GetIncludingFields().ToListAsync(cancellationToken);
    }

    protected virtual IQueryable<TDbEntity> GetIncludingFields()
    {
        return DbSet;
    }

    public async Task<TDbEntity> Add(TDbEntity entity, CancellationToken cancellationToken = default)
    {
        var resultEntry = await DbSet.AddAsync(await Map(entity, true), cancellationToken);
        await Context.SaveChangesAsync(cancellationToken);
        return resultEntry.Entity;
    }

    public async Task<TDbEntity> Update(TDbEntity entity, CancellationToken cancellationToken = default)
    {
        var entry = Context.Entry(await Map(entity, update: true));
        entry.State = EntityState.Modified;
        await Context.SaveChangesAsync(cancellationToken);

        return entry.Entity;
    }

    public async Task<TDbEntity> Delete(TDbEntity entity, CancellationToken cancellationToken = default)
    {
        var dbEntity = await Map(entity);
        DeleteRelationships(dbEntity);
        var returnEntity = DbSet.Remove(dbEntity).Entity;
        await Context.SaveChangesAsync(cancellationToken);
        return returnEntity;
    }

    protected abstract Task<TDbEntity> Map(TDbEntity domain, bool add = false, bool update = false);

    protected abstract void DeleteRelationships(TDbEntity entity);
}
