using StarWarsProgressBarIssueTracker.Domain.Issues;
using StarWarsProgressBarIssueTracker.Domain.Models;

namespace StarWarsProgressBarIssueTracker.Domain.Milestones;

public class Milestone : DomainBase
{
    public required string Title { get; set; }

    public string? Description { get; set; }

    public MilestoneState State { get; set; }

    public IEnumerable<Issue> Issues { get; set; } = [];
}
