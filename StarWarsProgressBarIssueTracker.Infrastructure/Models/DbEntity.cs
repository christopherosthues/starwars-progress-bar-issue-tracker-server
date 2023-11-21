namespace StarWarsProgressBarIssueTracker.Infrastructure.Models;

public abstract class DbEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public DateTime CreateAt { get; set; } = DateTime.UtcNow;

    public DateTime? LastModifiedAt { get; set; }
}
