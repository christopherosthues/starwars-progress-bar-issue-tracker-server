using System.ComponentModel.DataAnnotations.Schema;

namespace StarWarsProgressBarIssueTracker.Infrastructure.Models;

public record DbIssueExternalIds
{
    public Guid Id { get; set; }

    [ForeignKey("IssueId")]
    public required DbIssue Issue { get; set; }

    public string? GitlabId { get; set; }

    public string? GitlabIid { get; set; }

    public string? GitHubId { get; set; }
}
