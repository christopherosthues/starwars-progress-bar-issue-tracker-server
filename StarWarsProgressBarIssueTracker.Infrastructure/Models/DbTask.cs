namespace StarWarsProgressBarIssueTracker.Infrastructure.Models;

public record DbTask : DbEntityBase
{
    public required DbJob Job { get; set; }

    public TaskStatus Status { get; set; }

    public required DateTime ExecuteAt { get; set; }

    public DateTime? ExecutedAt { get; set; }
}
