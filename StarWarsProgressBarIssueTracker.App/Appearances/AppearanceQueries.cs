using StarWarsProgressBarIssueTracker.Domain.Appearances;

namespace StarWarsProgressBarIssueTracker.App.Appearances;

public class AppearanceQueries(IAppearanceService appearanceService)
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
