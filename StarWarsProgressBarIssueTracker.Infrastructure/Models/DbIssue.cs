using StarWarsProgressBarIssueTracker.Domain.Models;

namespace StarWarsProgressBarIssueTracker.Infrastructure.Models;

public class DbIssue : DbEntity
{
    public required string Title { get; set; }
    public string? Description { get; set; }
    public int Priority { get; set; }
    public IssueState IssueState { get; set; }
    public IssueType IssueType { get; set; }
    public List<DbLabel> Labels { get; set; } = [];
    public DbMilestone? Milestone { get; set; }
    public DbRelease? Release { get; set; }
    public DbVehicle? Vehicle { get; set; }
}
