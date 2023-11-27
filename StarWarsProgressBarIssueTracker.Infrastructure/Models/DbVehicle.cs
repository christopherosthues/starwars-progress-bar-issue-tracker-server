using System.ComponentModel.DataAnnotations.Schema;
using StarWarsProgressBarIssueTracker.Domain.Issues;

namespace StarWarsProgressBarIssueTracker.Infrastructure.Models;

[Table("Vehicles")]
public class DbVehicle
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Column("AppearanceId")]
    public virtual List<DbAppearance> Appearances { get; set; } = [];

    public EngineColor EngineColor { get; set; }

    [Column("TranslationId")]
    public virtual List<DbTranslation> Translations { get; set; } = [];

    [Column("PhotoId")]
    public virtual List<DbPhoto> Photos { get; set; } = [];
}
