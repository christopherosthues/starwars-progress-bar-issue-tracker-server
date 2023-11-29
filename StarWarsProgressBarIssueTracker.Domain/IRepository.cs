namespace StarWarsProgressBarIssueTracker.Domain;

public interface IRepository<T>
{
    Task<T?> GetById(Guid id);

    Task<IEnumerable<T>> GetAll();

    Task<T> Add(T entity);

    T Update(T entity);

    T Delete(T entity);
}
