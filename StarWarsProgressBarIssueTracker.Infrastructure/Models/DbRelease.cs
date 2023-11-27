using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using StarWarsProgressBarIssueTracker.Domain.Models;

namespace StarWarsProgressBarIssueTracker.Infrastructure.Models;

public class DbRelease : DbEntityBase
{
    [MaxLength(255)]
    public required string Title { get; set; }

    [MaxLength(500)]
    public string? ReleaseNotes { get; set; }

    public ReleaseState ReleaseState { get; set; }

    public DateTime? ReleaseDate { get; set; }

    [Column("IssueId")]
    public virtual List<DbIssue> Issues { get; set; } = [];
}
