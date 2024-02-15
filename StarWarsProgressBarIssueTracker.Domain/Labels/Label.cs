using StarWarsProgressBarIssueTracker.Domain.Models;

namespace StarWarsProgressBarIssueTracker.Domain.Labels;

public class Label : DomainBase
{
    public required string Title { get; set; }

    public string? Description { get; set; }

    public required string Color { get; set; }

    public required string TextColor { get; set; }
}
