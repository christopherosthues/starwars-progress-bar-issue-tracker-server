namespace StarWarsProgressBarIssueTracker.Domain.Releases;

public interface IReleaseService
{
    Task<IEnumerable<Release>> GetAllReleasesAsync(CancellationToken cancellationToken);

    Task<Release?> GetReleaseAsync(Guid id, CancellationToken cancellationToken);

    Task<Release> AddReleaseAsync(Release release, CancellationToken cancellationToken);

    Task<Release> UpdateReleaseAsync(Release release, CancellationToken cancellationToken);

    Task<Release> DeleteReleaseAsync(Guid id, CancellationToken cancellationToken);
}
