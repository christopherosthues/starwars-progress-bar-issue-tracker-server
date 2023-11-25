using System.ComponentModel.DataAnnotations.Schema;
using StarWarsProgressBarIssueTracker.Domain.Models;

namespace StarWarsProgressBarIssueTracker.Infrastructure.Models;

[Table("Vehicles")]
public class DbVehicle : DbEntity
{
    public EngineColor EngineColor { get; set; }
    public List<DbTranslation> Translations { get; set; } = [];
}
