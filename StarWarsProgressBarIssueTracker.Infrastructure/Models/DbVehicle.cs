using StarWarsProgressBarIssueTracker.Domain.Models;

namespace StarWarsProgressBarIssueTracker.Infrastructure.Models;

public class DbVehicle : DbEntity
{
    public EngineColor EngineColor { get; set; }
    public List<DbTranslation> Translations { get; set; } = [];
}
