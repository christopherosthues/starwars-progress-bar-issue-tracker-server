using StarWarsProgressBarIssueTracker.Domain.Labels;
using StarWarsProgressBarIssueTracker.Domain.Vehicles;

namespace StarWarsProgressBarIssueTracker.App.Queries;

public partial class IssueTrackerQueries
{
    public async Task<IEnumerable<Label>> GetLabels()
    {
        return await labelService.GetAllLabels();
    }

    public async Task<Label?> GetLabel(Guid id)
    {
        return await labelService.GetLabel(id);
    }
}
