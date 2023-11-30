namespace StarWarsProgressBarIssueTracker.Domain;

public interface IRepository<TDbDomain>
{
    Task<TDbDomain?> GetById(Guid id);

    Task<IEnumerable<TDbDomain>> GetAll();

    Task<TDbDomain> Add(TDbDomain domain);

    Task<TDbDomain> Update(TDbDomain entity);

    Task<TDbDomain> Delete(TDbDomain entity);
}
