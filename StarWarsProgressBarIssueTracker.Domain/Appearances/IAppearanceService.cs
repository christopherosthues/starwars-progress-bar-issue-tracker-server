namespace StarWarsProgressBarIssueTracker.Domain.Appearances;

public interface IAppearanceService
{
    Task<IEnumerable<Appearance>> GetAllAppearances();

    Task<Appearance> GetAppearance(Guid id);

    Task<Appearance> AddAppearance(string title, string color, string textColor, string? description);

    Appearance UpdateAppearance(Appearance appearance);

    Appearance DeleteAppearance(Appearance appearance);
}
