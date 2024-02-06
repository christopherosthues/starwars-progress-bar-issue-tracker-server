using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StarWarsProgressBarIssueTracker.Domain;
using StarWarsProgressBarIssueTracker.Infrastructure.Database;
using StarWarsProgressBarIssueTracker.Infrastructure.Models;

namespace StarWarsProgressBarIssueTracker.Infrastructure.Repositories;

public abstract class IssueTrackerRepositoryBase<TDomain, TDbEntity>(IssueTrackerContext context, IMapper mapper)
    : IRepository<TDomain>
    where TDbEntity : DbEntityBase
{
    protected readonly DbSet<TDbEntity> DbSet = context.Set<TDbEntity>();
    protected readonly IssueTrackerContext Context = context;

    public async Task<TDomain?> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        return mapper.Map<TDbEntity?, TDomain?>(await GetIncludingFields().FirstOrDefaultAsync(dbEntity => dbEntity.Id.Equals(id), cancellationToken));
    }

    public async Task<IEnumerable<TDomain>> GetAll(CancellationToken cancellationToken = default)
    {
        return mapper.Map<IEnumerable<TDbEntity>, IEnumerable<TDomain>>(await GetIncludingFields().ToListAsync(cancellationToken))!;
    }

    protected virtual IQueryable<TDbEntity> GetIncludingFields()
    {
        return DbSet;
    }

    public async Task<TDomain> Add(TDomain domain, CancellationToken cancellationToken = default)
    {
        var resultEntry = await DbSet.AddAsync(await Map(domain, true), cancellationToken);
        await Context.SaveChangesAsync(cancellationToken);
        return mapper.Map<TDbEntity, TDomain>(resultEntry.Entity)!;
    }

    public async Task<TDomain> Update(TDomain domain, CancellationToken cancellationToken = default)
    {
        var entry = Context.Entry(await Map(domain, update: true));
        entry.State = EntityState.Modified;
        await Context.SaveChangesAsync(cancellationToken);

        return mapper.Map<TDbEntity, TDomain>(entry.Entity)!;
    }

    public async Task<TDomain> Delete(TDomain domain, CancellationToken cancellationToken = default)
    {
        var dbEntity = await Map(domain);
        DeleteRelationships(dbEntity);
        var entity = DbSet.Remove(dbEntity).Entity;
        await Context.SaveChangesAsync(cancellationToken);
        return mapper.Map<TDbEntity, TDomain>(entity)!;
    }

    protected abstract Task<TDbEntity> Map(TDomain domain, bool add = false, bool update = false);

    protected abstract void DeleteRelationships(TDbEntity entity);
}
