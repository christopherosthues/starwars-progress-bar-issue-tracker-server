namespace StarWarsProgressBarIssueTracker.Domain.Models;

public abstract class EntityBase
{
    public Guid Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? LastModifiedAt { get; set; }
}
