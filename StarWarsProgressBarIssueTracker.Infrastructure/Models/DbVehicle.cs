using System.ComponentModel.DataAnnotations.Schema;
using StarWarsProgressBarIssueTracker.Domain.Issues;
using StarWarsProgressBarIssueTracker.Domain.Vehicles;

namespace StarWarsProgressBarIssueTracker.Infrastructure.Models;

public record DbVehicle
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [ForeignKey("AppearanceId")]
    public virtual List<DbAppearance> Appearances { get; set; } = [];

    public EngineColor EngineColor { get; set; }

    [ForeignKey("TranslationId")]
    public virtual List<DbTranslation> Translations { get; set; } = [];

    [ForeignKey("PhotoId")]
    public virtual List<DbPhoto> Photos { get; set; } = [];
}
