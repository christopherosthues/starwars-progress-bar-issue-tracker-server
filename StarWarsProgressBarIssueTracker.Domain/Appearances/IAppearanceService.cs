namespace StarWarsProgressBarIssueTracker.Domain.Appearances;

public interface IAppearanceService
{
    IEnumerable<Appearance> GetAllAppearances();

    Appearance GetAppearance(Guid id);

    void AddAppearance(Appearance appearance);

    void UpdateAppearance(Appearance appearance);

    void DeleteAppearance(Appearance appearance);
}
