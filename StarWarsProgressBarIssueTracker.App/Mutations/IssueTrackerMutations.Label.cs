using HotChocolate.Types;
using StarWarsProgressBarIssueTracker.Domain.Exceptions;
using StarWarsProgressBarIssueTracker.Domain.Labels;
using StarWarsProgressBarIssueTracker.Domain.Vehicles;

namespace StarWarsProgressBarIssueTracker.App.Mutations;

public partial class IssueTrackerMutations
{
    [Error<ValueNotSetException>]
    [Error<StringTooShortException>]
    [Error<StringTooLongException>]
    [Error<ColorFormatException>]
    public async Task<Label> AddLabel(string title, string color, string textColor, string? description)
    {
        return await labelService.AddLabel(new()
        {
            Title = title,
            Description = description,
            Color = color,
            TextColor = textColor
        });
    }

    [Error<ValueNotSetException>]
    [Error<StringTooShortException>]
    [Error<StringTooLongException>]
    [Error<ColorFormatException>]
    [Error<DomainIdNotFoundException>]
    public async Task<Label> UpdateLabel(Guid id, string title, string color, string textColor, string? description)
    {
        return await labelService.UpdateLabel(new()
        {
            Id = id,
            Title = title,
            Description = description,
            Color = color,
            TextColor = textColor
        });
    }

    [Error<DomainIdNotFoundException>]
    public async Task<Label> DeleteLabel(Guid id)
    {
        return await labelService.DeleteLabel(new()
        {
            Id = id,
            Title = string.Empty,
            Color = string.Empty,
            TextColor = string.Empty
        });
    }
}
