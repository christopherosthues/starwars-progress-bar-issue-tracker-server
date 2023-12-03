using StarWarsProgressBarIssueTracker.Domain.Milestones;

namespace StarWarsProgressBarIssueTracker.App.Queries;

public partial class IssueTrackerQueries
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
