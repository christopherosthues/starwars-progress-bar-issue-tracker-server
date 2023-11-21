using Microsoft.EntityFrameworkCore;
using StarWarsProgressBarIssueTracker.Domain;

namespace StarWarsProgressBarIssueTracker.Infrastructure;

public class IssueTrackerRepository<T>(DbContext context) : IRepository<T>
    where T : class
{
    // TODO: map domain entity to db entity

    private readonly DbSet<T> _dbSet = context.Set<T>();

    public T GetById(Guid id)
    {
        return _dbSet.Find(id)!;
    }

    public IEnumerable<T> GetAll()
    {
        return _dbSet.ToList();
    }

    public void Add(T entity)
    {
        _dbSet.Add(entity);
    }

    public void Update(T entity)
    {
        context.Entry(entity).State = EntityState.Modified;
    }

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }
}
