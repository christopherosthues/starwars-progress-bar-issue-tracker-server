using HotChocolate.Types;
using StarWarsProgressBarIssueTracker.Domain.Exceptions;
using StarWarsProgressBarIssueTracker.Domain.Milestones;

namespace StarWarsProgressBarIssueTracker.App.Mutations;

public partial class IssueTrackerMutations
{
    [Error<ValueNotSetException>]
    [Error<StringTooShortException>]
    [Error<StringTooLongException>]
    public async Task<Milestone> AddMilestone(string title, string? description)
    {
        return await milestoneService.AddMilestone(new()
        {
            Title = title,
            Description = description,
            MilestoneState = MilestoneState.Open,
        });
    }

    [Error<ValueNotSetException>]
    [Error<StringTooShortException>]
    [Error<StringTooLongException>]
    [Error<DomainIdNotFoundException>]
    public async Task<Milestone> UpdateMilestone(Guid id, string title, MilestoneState state, string? description)
    {
        return await milestoneService.UpdateMilestone(new Milestone
        {
            Id = id,
            Title = title,
            Description = description,
            MilestoneState = state
        });
    }

    [Error<DomainIdNotFoundException>]
    public async Task<Milestone> DeleteMilestone(Guid id)
    {
        return await milestoneService.DeleteMilestone(new Milestone
        {
            Id = id,
            Title = string.Empty
        });
    }
}
