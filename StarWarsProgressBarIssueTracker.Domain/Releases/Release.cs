using StarWarsProgressBarIssueTracker.Domain.Issues;
using StarWarsProgressBarIssueTracker.Domain.Models;

namespace StarWarsProgressBarIssueTracker.Domain.Releases;

public class Release : DomainBase
{
    public required string Title { get; set; }

    public string? Notes { get; set; }

    public ReleaseState State { get; set; }

    public DateTime? Date { get; set; }

    public IEnumerable<Issue> Issues { get; set; } = [];
}
