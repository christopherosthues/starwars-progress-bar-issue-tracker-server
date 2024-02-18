using System.ComponentModel.DataAnnotations.Schema;

namespace StarWarsProgressBarIssueTracker.Infrastructure.Models;

public record DbLabelExternalIds
{
    public Guid Id { get; set; }

    [ForeignKey("LabelId")]
    public required DbLabel Label { get; set; }

    public string? GitlabId { get; set; }

    public string? GitHubId { get; set; }
}
