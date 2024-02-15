namespace StarWarsProgressBarIssueTracker.Infrastructure.Repositories;

public interface IRepository<TEntity>
{
    Task<TEntity?> GetById(Guid id, CancellationToken cancellationToken = default);

    Task<IEnumerable<TEntity>> GetAll(CancellationToken cancellationToken = default);

    Task<TEntity> Add(TEntity entity, CancellationToken cancellationToken = default);

    Task<TEntity> Update(TEntity entity, CancellationToken cancellationToken = default);

    Task<TEntity> Delete(TEntity entity, CancellationToken cancellationToken = default);
}
