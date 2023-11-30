namespace StarWarsProgressBarIssueTracker.Domain.Appearances;

public interface IAppearanceService
{
    Task<IEnumerable<Appearance>> GetAllAppearances();

    Task<Appearance?> GetAppearance(Guid id);

    Task<Appearance> AddAppearance(Appearance appearance);

    Task<Appearance> UpdateAppearance(Appearance appearance);

    Task<Appearance> DeleteAppearance(Appearance appearance);
}
