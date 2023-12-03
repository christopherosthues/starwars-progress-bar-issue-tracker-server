using StarWarsProgressBarIssueTracker.Domain.Appearances;

namespace StarWarsProgressBarIssueTracker.App.Queries;

public partial class IssueTrackerQueries
{
    public async Task<IEnumerable<Appearance>> GetAppearances()
    {
        return await appearanceService.GetAllAppearances();
    }

    public async Task<Appearance?> GetAppearance(Guid id)
    {
        return await appearanceService.GetAppearance(id);
    }
}
