using StarWarsProgressBarIssueTracker.Domain.Appearances;

namespace StarWarsProgressBarIssueTracker.App.Appearances;

public class AppearanceMutations
{
    private readonly IAppearanceService _appearanceService;

    public AppearanceMutations(IAppearanceService appearanceService)
    {
        _appearanceService = appearanceService;
    }

    public async Task AddAppearance(Appearance appearance)
    {
        _appearanceService.AddAppearance(appearance);

        await Task.CompletedTask;
    }
}
