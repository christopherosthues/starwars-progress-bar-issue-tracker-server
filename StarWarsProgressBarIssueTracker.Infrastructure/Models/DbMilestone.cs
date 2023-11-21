using StarWarsProgressBarIssueTracker.Domain.Models;

namespace StarWarsProgressBarIssueTracker.Infrastructure.Models;

public class DbMilestone : DbEntity
{
    public required string Title { get; set; }
    public string? Description { get; set; }
    public MilestoneState MilestoneState { get; set; }
    public List<DbIssue> Issues { get; set; } = [];
}
