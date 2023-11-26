using System.ComponentModel.DataAnnotations.Schema;
using StarWarsProgressBarIssueTracker.Domain.Models;

namespace StarWarsProgressBarIssueTracker.Infrastructure.Models;

[Table("Vehicles")]
public class DbVehicle
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public List<DbAppearance> Appearances { get; set; } = [];

    public EngineColor EngineColor { get; set; }

    public List<DbTranslation> Translations { get; set; } = [];

    public List<DbPhoto> Photos { get; set; } = [];
}
