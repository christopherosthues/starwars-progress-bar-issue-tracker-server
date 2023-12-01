using StarWarsProgressBarIssueTracker.Domain.Milestones;

namespace StarWarsProgressBarIssueTracker.App.Milestones;

public class MilestoneQueries(IMilestoneService milestoneService)
{
    public async Task<IEnumerable<Milestone>> GetMilestones()
    {
        return await milestoneService.GetAllMilestones();
    }

    public async Task<Milestone?> GetMilestone(Guid id)
    {
        return await milestoneService.GetMilestone(id);
    }
}
