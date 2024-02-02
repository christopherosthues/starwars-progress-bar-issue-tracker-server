using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using StarWarsProgressBarIssueTracker.Domain.Releases;

namespace StarWarsProgressBarIssueTracker.Infrastructure.Models;

public record DbRelease : DbEntityBase
{
    [StringLength(ReleaseConstants.MaxTitleLength, MinimumLength = ReleaseConstants.MinTitleLength)]
    public required string Title { get; set; }

    [MaxLength(ReleaseConstants.MaxDescriptionLength)]
    public string? ReleaseNotes { get; set; }

    public ReleaseState ReleaseState { get; set; }

    public DateTime? ReleaseDate { get; set; }

    [Column("IssueId")]
    public virtual List<DbIssue> Issues { get; set; } = [];
}
