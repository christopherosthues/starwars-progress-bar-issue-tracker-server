using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using StarWarsProgressBarIssueTracker.Domain.Issues;

namespace StarWarsProgressBarIssueTracker.Infrastructure.Models;

public class DbIssue : DbEntityBase
{
    [MaxLength(255)]
    public required string Title { get; set; }

    [MaxLength(500)]
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
}
