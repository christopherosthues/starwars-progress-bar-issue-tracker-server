using StarWarsProgressBarIssueTracker.Domain.Models;

namespace StarWarsProgressBarIssueTracker.App.Queries;

public class LabelQueries
{
    public Label GetLabel() => new Label
    {
        Color = "Color", Title = "Title", TextColor = "TextColor", Description = "Description"
    };
}
