using System.ComponentModel.DataAnnotations;

namespace StarWarsProgressBarIssueTracker.Infrastructure.Models;

public class DbAppearance : DbEntityBase
{
    [MaxLength(50)]
    public required string Title { get; set; }

    [MaxLength(255)]
    public string? Description { get; set; }

    [StringLength(8, MinimumLength = 8)]
    public required string Color { get; set; }

    [StringLength(8, MinimumLength = 8)]
    public required string TextColor { get; set; }
}
