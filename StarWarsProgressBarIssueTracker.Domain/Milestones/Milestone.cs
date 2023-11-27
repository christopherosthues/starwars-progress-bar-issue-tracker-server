using StarWarsProgressBarIssueTracker.Domain.Issues;
using StarWarsProgressBarIssueTracker.Domain.Models;

namespace StarWarsProgressBarIssueTracker.Domain.Milestones;

public class Milestone : EntityBase
{
    public required string Title { get; set; }

    public string? Description { get; set; }

    public MilestoneState MilestoneState { get; set; }

    public IEnumerable<Issue> Issues { get; set; } = [];
}
