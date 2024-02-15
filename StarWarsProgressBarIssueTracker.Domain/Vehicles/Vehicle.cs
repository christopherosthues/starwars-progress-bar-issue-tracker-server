using StarWarsProgressBarIssueTracker.Domain.Models;

namespace StarWarsProgressBarIssueTracker.Domain.Vehicles;

public class Vehicle : DomainBase
{
    public IEnumerable<Appearance> Appearances { get; set; } = [];

    public EngineColor EngineColor { get; set; }

    public IEnumerable<Translation> Translations { get; set; } = [];

    public IEnumerable<Photo> Photos { get; set; } = [];
}
