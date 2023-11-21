namespace StarWarsProgressBarIssueTracker.Infrastructure.Models;

public record DbTranslation
{
    public required string Country { get; set; }
    public required string Text { get; set; }
}
