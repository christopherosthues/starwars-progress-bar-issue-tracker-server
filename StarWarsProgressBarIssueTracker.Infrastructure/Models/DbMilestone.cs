using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using StarWarsProgressBarIssueTracker.Domain.Milestones;

namespace StarWarsProgressBarIssueTracker.Infrastructure.Models;

public class DbMilestone : DbEntityBase
{
    [MaxLength(50)]
    public required string Title { get; set; }

    [MaxLength(255)]
    public string? Description { get; set; }

    public MilestoneState MilestoneState { get; set; }

    [Column("IssueId")]
    public virtual List<DbIssue> Issues { get; set; } = [];
}
