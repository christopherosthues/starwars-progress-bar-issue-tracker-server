using StarWarsProgressBarIssueTracker.Domain.Models;

namespace StarWarsProgressBarIssueTracker.Infrastructure.Models;

public class DbRelease : DbEntity
{
    public required string Title { get; set; }
    public string? ReleaseNotes { get; set; }
    public ReleaseState ReleaseState { get; set; }
    public DateTime? ReleaseDate { get; set; }
}
