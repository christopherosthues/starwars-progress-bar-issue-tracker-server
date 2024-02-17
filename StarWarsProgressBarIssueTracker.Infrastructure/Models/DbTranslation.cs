using System.ComponentModel.DataAnnotations;
using StarWarsProgressBarIssueTracker.Domain.Vehicles;

namespace StarWarsProgressBarIssueTracker.Infrastructure.Models;

public record DbTranslation : DbEntityBase
{
    [StringLength(TranslationConstants.MaxCountryLength, MinimumLength = TranslationConstants.MinCountryLength)]
    public required string Country { get; set; }

    [MaxLength(TranslationConstants.MaxTextLength)]
    public required string Text { get; set; }
}
