namespace StarWarsProgressBarIssueTracker.Domain.Models;

public class Issue : EntityBase
{
    public required string Title { get; set; }

    public string? Description { get; set; }

    public Priority Priority { get; set; }

    public IssueState IssueState { get; set; }

    public IssueType IssueType { get; set; }

    public Milestone? Milestone { get; set; }

    public Release? Release { get; set; }

    public Vehicle? Vehicle { get; set; }
}
