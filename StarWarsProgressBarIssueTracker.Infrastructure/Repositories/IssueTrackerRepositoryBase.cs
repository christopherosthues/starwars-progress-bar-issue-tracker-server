using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StarWarsProgressBarIssueTracker.Domain;
using StarWarsProgressBarIssueTracker.Infrastructure.Database;

namespace StarWarsProgressBarIssueTracker.Infrastructure.Repositories;

public abstract class IssueTrackerRepositoryBase<TDomain, TDbEntity> : IRepository<TDomain>
    where TDbEntity : class
{
    private readonly IssueTrackerContext _context;
    private readonly IMapper _mapper;
    protected readonly DbSet<TDbEntity> DbSet;

    public IssueTrackerRepositoryBase(IssueTrackerContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
        DbSet = context.Set<TDbEntity>();
    }

    public async Task<TDomain?> GetById(Guid id)
    {
        return _mapper.Map<TDbEntity?, TDomain?>(await DbSet.FindAsync(id));
    }

    public async Task<IEnumerable<TDomain>> GetAll()
    {
        return _mapper.Map<IEnumerable<TDbEntity>, IEnumerable<TDomain>>(await DbSet.ToListAsync());
    }

    public async Task<TDomain> Add(TDomain domain)
    {
        var resultEntry = await DbSet.AddAsync(await Map(domain, true));
        await _context.SaveChangesAsync();
        return _mapper.Map<TDbEntity, TDomain>(resultEntry.Entity);
    }

    public async Task<TDomain> Update(TDomain domain)
    {
        var entry = _context.Entry(await Map(domain, update: true));
        entry.State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return _mapper.Map<TDbEntity, TDomain>(entry.Entity);
    }

    public async Task<TDomain> Delete(TDomain domain)
    {
        var entity = DbSet.Remove(await Map(domain)).Entity;
        await _context.SaveChangesAsync();
        return _mapper.Map<TDbEntity, TDomain>(entity);
    }

    protected abstract Task<TDbEntity> Map(TDomain domain, bool add = false, bool update = false);
}
