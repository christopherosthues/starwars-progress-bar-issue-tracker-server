using System.ComponentModel.DataAnnotations.Schema;
using StarWarsProgressBarIssueTracker.Domain.Issues;

namespace StarWarsProgressBarIssueTracker.Infrastructure.Models;

public class DbVehicle
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
