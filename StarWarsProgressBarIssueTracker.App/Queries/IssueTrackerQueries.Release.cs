using StarWarsProgressBarIssueTracker.Domain.Releases;

namespace StarWarsProgressBarIssueTracker.App.Queries;

public partial class IssueTrackerQueries
{
    public async Task<IEnumerable<Release>> GetReleases()
    {
        return await releaseService.GetAllReleases();
    }

    public async Task<Release?> GetRelease(Guid id)
    {
        return await releaseService.GetRelease(id);
    }
}
