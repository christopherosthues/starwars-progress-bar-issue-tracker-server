using System.ComponentModel.DataAnnotations.Schema;
using StarWarsProgressBarIssueTracker.Domain.Issues;
using StarWarsProgressBarIssueTracker.Domain.Vehicles;

namespace StarWarsProgressBarIssueTracker.Infrastructure.Models;

public record DbVehicle : DbEntityBase
{
    [ForeignKey("AppearanceId")]
    public virtual IList<DbAppearance> Appearances { get; set; } = [];

    public EngineColor EngineColor { get; set; }

    [ForeignKey("TranslationId")]
    public virtual IList<DbTranslation> Translations { get; set; } = [];

    [ForeignKey("PhotoId")]
    public virtual IList<DbPhoto> Photos { get; set; } = [];
}
