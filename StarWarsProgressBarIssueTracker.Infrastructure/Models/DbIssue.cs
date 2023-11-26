using System.ComponentModel.DataAnnotations;
using StarWarsProgressBarIssueTracker.Domain.Models;

namespace StarWarsProgressBarIssueTracker.Infrastructure.Models;

public class DbIssue : DbEntityBase
{
    [MaxLength(255)]
    public required string Title { get; set; }

    [MaxLength(500)]
    public string? Description { get; set; }

    public int Priority { get; set; }

    public IssueState IssueState { get; set; }

    public IssueType IssueType { get; set; }

    public DbMilestone? Milestone { get; set; }

    public DbRelease? Release { get; set; }

    public DbVehicle? Vehicle { get; set; }
}
