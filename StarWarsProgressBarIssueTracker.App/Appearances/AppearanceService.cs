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
        if (string.IsNullOrWhiteSpace(appearance.Title))
        {
            throw new ValueNotSetException(nameof(Appearance.Title));
        }

        if (appearance.Title.Length < 1)
        {
            throw new StringTooShortException(appearance.Title, nameof(Appearance.Title),
                $"The length of {nameof(Appearance.Title)} has to be between 1 and 50.");
        }

        if (appearance.Title.Length > 50)
        {
            throw new StringTooLongException(appearance.Title, nameof(Appearance.Title),
                $"The length of {nameof(Appearance.Title)} has to be between 1 and 50.");
        }

        if (appearance.Description is not null && appearance.Description.Length > 255)
        {
            throw new StringTooLongException(appearance.Description, nameof(Appearance.Description),
                $"The length of {nameof(Appearance.Description)} has to be less than 256.");
        }

        if (string.IsNullOrWhiteSpace(appearance.Color))
        {
            throw new ValueNotSetException(nameof(Appearance.Color));
        }

        var regex = @"^[a-fA-F0-9]{6}$";
        var regexMatcher = new Regex(regex);
        if (!regexMatcher.Match(appearance.Color).Success)
        {
            throw new ColorFormatException(appearance.Color, nameof(Appearance.Color));
        }

        if (string.IsNullOrWhiteSpace(appearance.TextColor))
        {
            throw new ValueNotSetException(nameof(Appearance.TextColor));
        }

        if (!regexMatcher.Match(appearance.TextColor).Success)
        {
            throw new ColorFormatException(appearance.TextColor, nameof(Appearance.TextColor));
        }

        return repository.Add(appearance);
    }

    public Task<Appearance> UpdateAppearance(Appearance appearance)
    {
        return repository.Update(appearance);
    }

    public Task<Appearance> DeleteAppearance(Appearance appearance)
    {
        return repository.Delete(appearance);
    }
}
