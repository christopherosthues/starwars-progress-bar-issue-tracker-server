namespace StarWarsProgressBarIssueTracker.Domain.Models;

public abstract class EntityBase
{
    public required Guid Id { get; set; }

    public required DateTime CreateAt { get; set; }

    public DateTime? LastModifiedAt { get; set; }
}
