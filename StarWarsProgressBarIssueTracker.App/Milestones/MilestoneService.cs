using StarWarsProgressBarIssueTracker.Domain.Milestones;
using StarWarsProgressBarIssueTracker.Infrastructure.Repositories;

namespace StarWarsProgressBarIssueTracker.App.Milestones;

public class MilestoneService(IMilestoneRepository repository) : IMilestoneService
{
    public Task<IEnumerable<Milestone>> GetAllMilestones()
    {
        return repository.GetAll();
    }

    public Task<Milestone?> GetMilestone(Guid id)
    {
        return repository.GetById(id);
    }

    public Task<Milestone> AddMilestone(Milestone milestone)
    {
        return repository.Add(milestone);
    }

    public Task<Milestone> UpdateMilestone(Milestone milestone)
    {
        return repository.Update(milestone);
    }

    public Task<Milestone> DeleteMilestone(Milestone milestone)
    {
        return repository.Delete(milestone);
    }
}
