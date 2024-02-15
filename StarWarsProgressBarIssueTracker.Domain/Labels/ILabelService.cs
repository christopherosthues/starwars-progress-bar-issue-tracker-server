namespace StarWarsProgressBarIssueTracker.Domain.Labels;

public interface ILabelService
{
    Task<IEnumerable<Label>> GetAllLabels();

    Task<Label?> GetLabel(Guid id);

    Task<Label> AddLabel(Label label);

    Task<Label> UpdateLabel(Label label);

    Task<Label> DeleteLabel(Label label);
}
