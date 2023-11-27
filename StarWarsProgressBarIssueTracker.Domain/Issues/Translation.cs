namespace StarWarsProgressBarIssueTracker.Domain.Issues;

public class Translation
{
    public Guid Id { get; set; }

    public required string Country { get; set; }

    public required string Text { get; set; }
}
