using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using StarWarsProgressBarIssueTracker.Domain.Labels;

namespace StarWarsProgressBarIssueTracker.Infrastructure.Models;

public record DbLabel : DbEntityBase
{
    [StringLength(LabelConstants.MaxTitleLength, MinimumLength = LabelConstants.MinTitleLength)]
    public required string Title { get; set; }

    [MaxLength(LabelConstants.MaxDescriptionLength)]
    public string? Description { get; set; }

    [StringLength(LabelConstants.ColorHexLength, MinimumLength = LabelConstants.ColorHexLength)]
    public required string Color { get; set; }

    [StringLength(LabelConstants.ColorHexLength, MinimumLength = LabelConstants.ColorHexLength)]
    public required string TextColor { get; set; }

    [ForeignKey("IssueId")]
    [DeleteBehavior(DeleteBehavior.SetNull)]
    public virtual IList<DbIssue> Issues { get; set; } = [];

    public string? GitlabId { get; set; }

    public string? GitHubId { get; set; }
}
