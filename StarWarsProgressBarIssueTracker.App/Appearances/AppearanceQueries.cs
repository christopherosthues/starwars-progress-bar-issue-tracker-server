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
        return new List<Appearance>
        {
            new()
            {
                Id = Guid.NewGuid(),
                CreateAt = DateTime.UtcNow,
                Color = "Color",
                Title = "Title",
                TextColor = "TextColor",
                Description = "Description"
            }
        };
    }

    public Appearance GetAppearance(Guid id)
    {
        return new()
        {
            Id = id,
            CreateAt = DateTime.UtcNow,
            Color = "Color",
            Title = "Title",
            TextColor = "TextColor",
            Description = "Description"
        };
    }
}
