using StarWarsProgressBarIssueTracker.Domain.Appearances;

namespace StarWarsProgressBarIssueTracker.App.Appearances;

public class AppearanceMutations(IAppearanceService appearanceService)
{
    public async Task<Appearance> AddAppearance(string title, string color, string textColor, string? description)
    {
        return await appearanceService.AddAppearance(new()
        {
            Title = title,
            Description = description,
            Color = color,
            TextColor = textColor
        });
    }

    public async Task<Appearance> UpdateAppearance(Guid id, string title, string color, string textColor, string? description)
    {
        return await appearanceService.UpdateAppearance(new Appearance
        {
            Id = id,
            Title = title,
            Description = description,
            Color = color,
            TextColor = textColor
        });
    }

    public async Task<Appearance> DeleteAppearance(Guid id)
    {
        return await appearanceService.DeleteAppearance(new Appearance
        {
            Id = id,
            Title = string.Empty,
            Color = string.Empty,
            TextColor = string.Empty
        });
    }
}
