namespace StarWarsProgressBarIssueTracker.Domain.Models;

public class Release : EntityBase
{
    public required string Title { get; set; }

    public string? ReleaseNotes { get; set; }

    public ReleaseState ReleaseState { get; set; }

    public DateTime? ReleaseDate { get; set; }

    public IEnumerable<Issue> Issues { get; set; } = [];
}
