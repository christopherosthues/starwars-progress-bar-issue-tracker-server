namespace StarWarsProgressBarIssueTracker.Domain.Milestones;

public interface IMilestoneService
{
    public Task<IEnumerable<Milestone>> GetAllMilestones();

    public Task<Milestone?> GetMilestone(Guid id);

    public Task<Milestone> AddMilestone(Milestone milestone);

    public Task<Milestone> UpdateMilestone(Milestone milestone);

    public Task<Milestone> DeleteMilestone(Milestone milestone);
}
