using StarWarsProgressBarIssueTracker.Domain.Models;

namespace StarWarsProgressBarIssueTracker.Domain.Issues;

public class Photo : EntityBase
{
    public required string PhotoData { get; set; }
}
