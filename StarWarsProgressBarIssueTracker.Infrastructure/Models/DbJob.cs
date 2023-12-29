namespace StarWarsProgressBarIssueTracker.Infrastructure.Models;

public record DbJob : DbEntityBase
{
    public required string CronInterval { get; set; }

    public bool IsPaused { get; set; }

    public DateTime? NextExecution { get; set; }
}
