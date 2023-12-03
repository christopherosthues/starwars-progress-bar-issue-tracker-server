using System.ComponentModel.DataAnnotations;

namespace StarWarsProgressBarIssueTracker.Infrastructure.Models;

public class DbAppearance : DbEntityBase
{
    [StringLength(50, MinimumLength = 1)]
    public required string Title { get; set; }

    [MaxLength(255)]
    public string? Description { get; set; }

    [StringLength(6, MinimumLength = 6)]
    public required string Color { get; set; }

    [StringLength(6, MinimumLength = 6)]
    public required string TextColor { get; set; }
}
