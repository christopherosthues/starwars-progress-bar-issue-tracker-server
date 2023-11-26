namespace StarWarsProgressBarIssueTracker.Domain.Models;

public class Milestone : EntityBase
{
    public required string Title { get; set; }

    public string? Description { get; set; }

    public MilestoneState MilestoneState { get; set; }

    public IEnumerable<Issue> Issues { get; set; } = [];
}
