namespace StarWarsProgressBarIssueTracker.Infrastructure.Models;

public abstract class DbEntityBase
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? LastModifiedAt { get; set; }
}
