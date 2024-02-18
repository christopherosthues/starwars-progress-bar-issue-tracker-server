using System.ComponentModel.DataAnnotations.Schema;

namespace StarWarsProgressBarIssueTracker.Infrastructure.Models;

public record DbMilestoneExternalIds
{
    public Guid Id { get; set; }

    [ForeignKey("MilestoneId")]
    public required DbMilestone Milestone { get; set; }

    public string? GitlabId { get; set; }

    public string? GitlabIid { get; set; }

    public string? GitHubId { get; set; }
}
