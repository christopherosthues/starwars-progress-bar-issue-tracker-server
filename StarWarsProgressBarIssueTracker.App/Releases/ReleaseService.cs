using StarWarsProgressBarIssueTracker.Domain.Releases;
using StarWarsProgressBarIssueTracker.Infrastructure.Repositories;

namespace StarWarsProgressBarIssueTracker.App.Releases;

public class ReleaseService(IReleaseRepository repository) : IReleaseService
{
    public Task<IEnumerable<Release>> GetAllReleases()
    {
        return repository.GetAll();
    }

    public Task<Release?> GetRelease(Guid id)
    {
        return repository.GetById(id);
    }

    public Task<Release> AddRelease(Release release)
    {
        return repository.Add(release);
    }

    public Task<Release> UpdateRelease(Release release)
    {
        return repository.Update(release);
    }

    public Task<Release> DeleteRelease(Release release)
    {
        return repository.Delete(release);
    }
}
