using HotChocolate.Types;
using StarWarsProgressBarIssueTracker.Domain.Appearances;
using StarWarsProgressBarIssueTracker.Domain.Exceptions;

namespace StarWarsProgressBarIssueTracker.App.Mutations;

public partial class IssueTrackerMutations
{
    [Error<ValueNotSetException>]
    [Error<StringTooShortException>]
    [Error<StringTooLongException>]
    [Error<ColorFormatException>]
    public async Task<Appearance> AddAppearance(string title, string color, string textColor, string? description)
    {
        return await appearanceService.AddAppearance(new()
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
    public async Task<Appearance> UpdateAppearance(Guid id, string title, string color, string textColor, string? description)
    {
        return await appearanceService.UpdateAppearance(new Appearance
        {
            Id = id,
            Title = title,
            Description = description,
            Color = color,
            TextColor = textColor
        });
    }

    [Error<DomainIdNotFoundException>]
    public async Task<Appearance> DeleteAppearance(Guid id)
    {
        return await appearanceService.DeleteAppearance(new Appearance
        {
            Id = id,
            Title = string.Empty,
            Color = string.Empty,
            TextColor = string.Empty
        });
    }
}
