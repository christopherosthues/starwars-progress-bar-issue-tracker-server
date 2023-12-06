namespace StarWarsProgressBarIssueTracker.Infrastructure.Models;

public abstract record DbEntityBase
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? LastModifiedAt { get; set; }
}
