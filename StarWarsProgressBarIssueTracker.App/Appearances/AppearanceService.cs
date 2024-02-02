using System.Text.RegularExpressions;
using StarWarsProgressBarIssueTracker.Domain.Appearances;
using StarWarsProgressBarIssueTracker.Domain.Exceptions;
using StarWarsProgressBarIssueTracker.Infrastructure.Repositories;

namespace StarWarsProgressBarIssueTracker.App.Appearances;

public class AppearanceService(IAppearanceRepository repository) : IAppearanceService
{
    public Task<IEnumerable<Appearance>> GetAllAppearances()
    {
        return repository.GetAll();
    }

    public Task<Appearance?> GetAppearance(Guid id)
    {
        return repository.GetById(id);
    }

    public Task<Appearance> AddAppearance(Appearance appearance)
    {
        ValidateAppearance(appearance);

        return repository.Add(appearance);
    }

    private static void ValidateAppearance(Appearance appearance)
    {
        var errors = new List<Exception>();
        if (string.IsNullOrWhiteSpace(appearance.Title))
        {
            errors.Add(new ValueNotSetException(nameof(Appearance.Title)));
        }

        if (appearance.Title.Length < AppearanceConstants.MinTitleLength)
        {
            errors.Add(new StringTooShortException(appearance.Title, nameof(Appearance.Title),
                $"The length of {nameof(Appearance.Title)} has to be between {AppearanceConstants.MinTitleLength} and {AppearanceConstants.MaxTitleLength}."));
        }

        if (appearance.Title.Length > AppearanceConstants.MaxTitleLength)
        {
            errors.Add(new StringTooLongException(appearance.Title, nameof(Appearance.Title),
                $"The length of {nameof(Appearance.Title)} has to be between {AppearanceConstants.MinTitleLength} and {AppearanceConstants.MaxTitleLength}."));
        }

        if (appearance.Description is not null && appearance.Description.Length > AppearanceConstants.MaxDescriptionLength)
        {
            errors.Add(new StringTooLongException(appearance.Description, nameof(Appearance.Description),
                $"The length of {nameof(Appearance.Description)} has to be less than {AppearanceConstants.MaxDescriptionLength + 1}."));
        }

        if (string.IsNullOrWhiteSpace(appearance.Color))
        {
            errors.Add(new ValueNotSetException(nameof(Appearance.Color)));
        }

        var regex = @"^[a-fA-F0-9]{6}$";
        var regexMatcher = new Regex(regex);
        if (!regexMatcher.Match(appearance.Color).Success)
        {
            errors.Add(new ColorFormatException(appearance.Color, nameof(Appearance.Color)));
        }

        if (string.IsNullOrWhiteSpace(appearance.TextColor))
        {
            errors.Add(new ValueNotSetException(nameof(Appearance.TextColor)));
        }

        if (!regexMatcher.Match(appearance.TextColor).Success)
        {
            errors.Add(new ColorFormatException(appearance.TextColor, nameof(Appearance.TextColor)));
        }

        if (errors.Count != 0)
        {
            throw new AggregateException(errors);
        }
    }

    public Task<Appearance> UpdateAppearance(Appearance appearance)
    {
        ValidateAppearance(appearance);
        return repository.Update(appearance);
    }

    public Task<Appearance> DeleteAppearance(Appearance appearance)
    {
        return repository.Delete(appearance);
    }
}
