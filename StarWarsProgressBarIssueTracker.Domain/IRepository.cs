namespace StarWarsProgressBarIssueTracker.Domain;

public interface IRepository<TDbDomain>
{
    Task<TDbDomain?> GetById(Guid id, CancellationToken cancellationToken = default);

    Task<IEnumerable<TDbDomain>> GetAll(CancellationToken cancellationToken = default);

    Task<TDbDomain> Add(TDbDomain domain, CancellationToken cancellationToken = default);

    Task<TDbDomain> Update(TDbDomain entity, CancellationToken cancellationToken = default);

    Task<TDbDomain> Delete(TDbDomain entity, CancellationToken cancellationToken = default);
}
