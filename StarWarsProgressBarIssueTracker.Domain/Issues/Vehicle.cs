using StarWarsProgressBarIssueTracker.Domain.Appearances;

namespace StarWarsProgressBarIssueTracker.Domain.Issues;

public class Vehicle
{
    public Guid Id { get; set; }

    public IEnumerable<Appearance> Appearances { get; set; } = [];

    public EngineColor EngineColor { get; set; }

    public IEnumerable<Translation> Translations { get; set; } = [];

    public IEnumerable<Photo> Photos { get; set; } = [];
}
