using StarWarsProgressBarIssueTracker.Domain.Appearances;
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
