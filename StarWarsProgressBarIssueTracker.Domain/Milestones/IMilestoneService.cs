namespace StarWarsProgressBarIssueTracker.Domain.Milestones;

public interface IMilestoneService
{
    Task<IEnumerable<Milestone>> GetAllMilestonesAsync(CancellationToken cancellationToken);

    Task<Milestone?> GetMilestoneAsync(Guid id, CancellationToken cancellationToken);

    Task<Milestone> AddMilestoneAsync(Milestone milestone, CancellationToken cancellationToken);

    Task<Milestone> UpdateMilestoneAsync(Milestone milestone, CancellationToken cancellationToken);

    Task<Milestone> DeleteMilestoneAsync(Guid id, CancellationToken cancellationToken);
}
