namespace StarWarsProgressBarIssueTracker.Domain.Models;

public abstract class EntityBase
{
    public Guid Id { get; set; }

    public DateTime CreateAt { get; set; }

    public DateTime? LastModifiedAt { get; set; }
}
