namespace StarWarsProgressBarIssueTracker.Infrastructure.Models;

public record DbPhoto : DbEntityBase
{
    public required byte[] PhotoData { get; set; }
}
