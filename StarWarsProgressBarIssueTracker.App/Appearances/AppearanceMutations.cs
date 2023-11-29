using StarWarsProgressBarIssueTracker.Domain.Appearances;

namespace StarWarsProgressBarIssueTracker.App.Appearances;

public class AppearanceMutations
{
    private readonly IAppearanceService _appearanceService;

    public AppearanceMutations(AppearanceService appearanceService)
    {
        _appearanceService = appearanceService;
    }

    public async Task<Appearance> AddAppearance(string title, string color, string textColor, string? description)
    {
        return await _appearanceService.AddAppearance(title, color, textColor, description);
    }
}
