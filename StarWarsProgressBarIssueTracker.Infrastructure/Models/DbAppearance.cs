using System.ComponentModel.DataAnnotations;
using StarWarsProgressBarIssueTracker.Domain.Appearances;

namespace StarWarsProgressBarIssueTracker.Infrastructure.Models;

public record DbAppearance : DbEntityBase
{
    [StringLength(AppearanceConstants.MaxTitleLength, MinimumLength = AppearanceConstants.MinTitleLength)]
    public required string Title { get; set; }

    [MaxLength(AppearanceConstants.MaxDescriptionLength)]
    public string? Description { get; set; }

    [StringLength(AppearanceConstants.ColorHexLength, MinimumLength = AppearanceConstants.ColorHexLength)]
    public required string Color { get; set; }

    [StringLength(AppearanceConstants.ColorHexLength, MinimumLength = AppearanceConstants.ColorHexLength)]
    public required string TextColor { get; set; }
}
