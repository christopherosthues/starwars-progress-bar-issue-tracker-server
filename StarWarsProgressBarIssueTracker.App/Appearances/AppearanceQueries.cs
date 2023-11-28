using StarWarsProgressBarIssueTracker.Domain.Appearances;

namespace StarWarsProgressBarIssueTracker.App.Appearances;

public class AppearanceQueries
{
    private readonly IAppearanceService _appearanceService;

    public AppearanceQueries(IAppearanceService appearanceService)
    {
        _appearanceService = appearanceService;
    }

    public IEnumerable<Appearance> GetAppearances()
    {
        return _appearanceService.GetAllAppearances();
    }

    public Appearance GetAppearance(Guid id)
    {
        return _appearanceService.GetAppearance(id);
    }
}
