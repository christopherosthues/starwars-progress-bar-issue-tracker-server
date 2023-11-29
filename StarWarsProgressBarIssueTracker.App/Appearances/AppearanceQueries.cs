using StarWarsProgressBarIssueTracker.Domain.Appearances;

namespace StarWarsProgressBarIssueTracker.App.Appearances;

public class AppearanceQueries
{
    private readonly IAppearanceService _appearanceService;

    public AppearanceQueries(AppearanceService appearanceService)
    {
        _appearanceService = appearanceService;
    }

    public async Task<IEnumerable<Appearance>> GetAppearances()
    {
        return await _appearanceService.GetAllAppearances();
    }

    public async Task<Appearance> GetAppearance(Guid id)
    {
        return await _appearanceService.GetAppearance(id);
    }
}
