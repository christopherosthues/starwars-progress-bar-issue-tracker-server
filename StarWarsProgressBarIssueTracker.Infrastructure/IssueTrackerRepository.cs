using Microsoft.EntityFrameworkCore;
using StarWarsProgressBarIssueTracker.Domain;
using StarWarsProgressBarIssueTracker.Infrastructure.Database;

namespace StarWarsProgressBarIssueTracker.Infrastructure;

public class IssueTrackerRepository<T> : IRepository<T>
    where T : class
{
    private readonly DbSet<T> _dbSet;
    private readonly IssueTrackerContext _context;

    public IssueTrackerRepository(IssueTrackerContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<T?> GetById(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<T> Add(T entity)
    {
        var resultEntry = await _dbSet.AddAsync(entity);
        return resultEntry.Entity;
    }

    public T Update(T entity)
    {
        var entry = _context.Entry(entity);
        entry.State = EntityState.Modified;

        return entry.Entity;
    }

    public T Delete(T entity)
    {
        return _dbSet.Remove(entity).Entity;
    }
}
