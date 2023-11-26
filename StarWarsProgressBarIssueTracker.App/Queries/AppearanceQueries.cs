using StarWarsProgressBarIssueTracker.Domain.Models;

namespace StarWarsProgressBarIssueTracker.App.Queries;

public class AppearanceQueries
{
    public Appearance GetLabel() => new Appearance
    {
        Id = Guid.NewGuid(), CreateAt = DateTime.UtcNow,
        Color = "Color", Title = "Title", TextColor = "TextColor", Description = "Description"
    };
}
