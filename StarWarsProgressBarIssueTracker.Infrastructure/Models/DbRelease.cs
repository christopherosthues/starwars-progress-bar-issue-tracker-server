using System.ComponentModel.DataAnnotations;
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

    public List<DbIssue> Issues { get; set; } = [];
}
