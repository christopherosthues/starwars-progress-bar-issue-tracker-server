using System.Text.RegularExpressions;
using StarWarsProgressBarIssueTracker.Domain.Exceptions;
using StarWarsProgressBarIssueTracker.Domain.Labels;
using StarWarsProgressBarIssueTracker.Domain.Vehicles;
using StarWarsProgressBarIssueTracker.Infrastructure.Repositories;

namespace StarWarsProgressBarIssueTracker.App.Labels;

public class LabelService(ILabelRepository repository) : ILabelService
{
    public Task<IEnumerable<Label>> GetAllLabels()
    {
        return repository.GetAll();
    }

    public Task<Label?> GetLabel(Guid id)
    {
        return repository.GetById(id);
    }

    public Task<Label> AddLabel(Label label)
    {
        ValidateLabel(label);

        return repository.Add(label);
    }

    private static void ValidateLabel(Label label)
    {
        var errors = new List<Exception>();
        if (string.IsNullOrWhiteSpace(label.Title))
        {
            errors.Add(new ValueNotSetException(nameof(Label.Title)));
        }

        if (label.Title.Length < LabelConstants.MinTitleLength)
        {
            errors.Add(new StringTooShortException(label.Title, nameof(Label.Title),
                $"The length of {nameof(Label.Title)} has to be between {LabelConstants.MinTitleLength} and {LabelConstants.MaxTitleLength}."));
        }

        if (label.Title.Length > LabelConstants.MaxTitleLength)
        {
            errors.Add(new StringTooLongException(label.Title, nameof(Label.Title),
                $"The length of {nameof(Label.Title)} has to be between {LabelConstants.MinTitleLength} and {LabelConstants.MaxTitleLength}."));
        }

        if (label.Description is not null && label.Description.Length > LabelConstants.MaxDescriptionLength)
        {
            errors.Add(new StringTooLongException(label.Description, nameof(Label.Description),
                $"The length of {nameof(Label.Description)} has to be less than {LabelConstants.MaxDescriptionLength + 1}."));
        }

        if (string.IsNullOrWhiteSpace(label.Color))
        {
            errors.Add(new ValueNotSetException(nameof(Appearance.Color)));
        }

        var regex = @"^[a-fA-F0-9]{6}$";
        var regexMatcher = new Regex(regex);
        if (!regexMatcher.Match(label.Color).Success)
        {
            errors.Add(new ColorFormatException(label.Color, nameof(Label.Color)));
        }

        if (string.IsNullOrWhiteSpace(label.TextColor))
        {
            errors.Add(new ValueNotSetException(nameof(Label.TextColor)));
        }

        if (!regexMatcher.Match(label.TextColor).Success)
        {
            errors.Add(new ColorFormatException(label.TextColor, nameof(Label.TextColor)));
        }

        if (errors.Count != 0)
        {
            throw new AggregateException(errors);
        }
    }

    public Task<Label> UpdateLabel(Label label)
    {
        ValidateLabel(label);
        return repository.Update(label);
    }

    public Task<Label> DeleteLabel(Label label)
    {
        return repository.Delete(label);
    }
}
