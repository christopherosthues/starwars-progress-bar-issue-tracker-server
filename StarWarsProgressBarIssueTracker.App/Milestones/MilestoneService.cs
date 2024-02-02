using StarWarsProgressBarIssueTracker.Domain.Exceptions;
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
        ValidateMilestone(milestone);

        return repository.Add(milestone);
    }

    private static void ValidateMilestone(Milestone milestone)
    {
        var errors = new List<Exception>();
        if (string.IsNullOrWhiteSpace(milestone.Title))
        {
            errors.Add(new ValueNotSetException(nameof(Milestone.Title)));
        }

        if (milestone.Title.Length < 1)
        {
            errors.Add(new StringTooShortException(milestone.Title, nameof(Milestone.Title),
                $"The length of {nameof(Milestone.Title)} has to be between {MilestoneConstants.MinTitleLength} and {MilestoneConstants.MaxTitleLength}."));
        }

        if (milestone.Title.Length > MilestoneConstants.MaxTitleLength)
        {
            errors.Add(new StringTooLongException(milestone.Title, nameof(Milestone.Title),
                $"The length of {nameof(Milestone.Title)} has to be between {MilestoneConstants.MinTitleLength} and {MilestoneConstants.MaxTitleLength}."));
        }

        if (milestone.Description is not null && milestone.Description.Length > MilestoneConstants.MaxDescriptionLength)
        {
            errors.Add(new StringTooLongException(milestone.Description, nameof(Milestone.Description),
                $"The length of {nameof(Milestone.Description)} has to be less than {MilestoneConstants.MaxDescriptionLength + 1}."));
        }

        if (!Enum.IsDefined(milestone.MilestoneState) || milestone.MilestoneState == MilestoneState.Unknown)
        {
            errors.Add(new ValueNotSetException(nameof(Milestone.MilestoneState)));
        }

        if (errors.Count != 0)
        {
            throw new AggregateException(errors);
        }
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
