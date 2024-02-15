namespace StarWarsProgressBarIssueTracker.Infrastructure.Models;

public record DbPhoto : DbEntityBase
{
    public required string FilePath { get; set; }
}
