namespace StarWarsProgressBarIssueTracker.Domain.Labels;

public interface ILabelService
{
    Task<IEnumerable<Label>> GetAllLabelsAsync(CancellationToken cancellationToken);

    Task<Label?> GetLabelAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Label> AddLabelAsync(Label label, CancellationToken cancellationToken = default);

    Task<Label> UpdateLabelAsync(Label label, CancellationToken cancellationToken = default);

    Task<Label> DeleteLabelAsync(Guid id, CancellationToken cancellationToken = default);

    Task SynchronizeAsync(IList<Label> labels, CancellationToken cancellationToken = default);
}
