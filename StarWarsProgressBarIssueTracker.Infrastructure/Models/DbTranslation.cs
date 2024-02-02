using System.ComponentModel.DataAnnotations;
using StarWarsProgressBarIssueTracker.Domain.Issues;

namespace StarWarsProgressBarIssueTracker.Infrastructure.Models;

public record DbTranslation
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [StringLength(TranslationConstants.MaxCountryLength, MinimumLength = TranslationConstants.MinCountryLength)]
    public required string Country { get; set; }

    [MaxLength(TranslationConstants.MaxTextLength)]
    public required string Text { get; set; }
}
