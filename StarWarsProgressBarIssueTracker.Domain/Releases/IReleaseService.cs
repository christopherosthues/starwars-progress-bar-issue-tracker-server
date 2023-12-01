namespace StarWarsProgressBarIssueTracker.Domain.Releases;

public interface IReleaseService
{
    public Task<IEnumerable<Release>> GetAllReleases();

    public Task<Release?> GetRelease(Guid id);

    public Task<Release> AddRelease(Release release);

    public Task<Release> UpdateRelease(Release release);

    public Task<Release> DeleteRelease(Release release);
}
