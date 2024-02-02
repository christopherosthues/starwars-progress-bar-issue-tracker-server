using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using StarWarsProgressBarIssueTracker.Domain.Issues;

namespace StarWarsProgressBarIssueTracker.Infrastructure.Models;

public record DbIssue : DbEntityBase
{
    [StringLength(IssueConstants.MaxTitleLength, MinimumLength = IssueConstants.MinTitleLength)]
    public required string Title { get; set; }

    [MaxLength(IssueConstants.MaxDescriptionLength)]
    public string? Description { get; set; }

    public Priority Priority { get; set; }

    public IssueState IssueState { get; set; }

    public IssueType IssueType { get; set; }

    [ForeignKey("MilestoneId")]
    public DbMilestone? Milestone { get; set; }

    [ForeignKey("ReleaseId")]
    public DbRelease? Release { get; set; }

    [ForeignKey("VehicleId")]
    public DbVehicle? Vehicle { get; set; }

    public IList<DbIssue> RelatedIssues { get; set; } = [];
}
