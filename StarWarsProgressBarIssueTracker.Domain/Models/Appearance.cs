namespace StarWarsProgressBarIssueTracker.Domain.Models;

public class Appearance : EntityBase
{
    public required string Title { get; set; }

    public string? Description { get; set; }

    public required string Color { get; set; }

    public required string TextColor { get; set; }
}
