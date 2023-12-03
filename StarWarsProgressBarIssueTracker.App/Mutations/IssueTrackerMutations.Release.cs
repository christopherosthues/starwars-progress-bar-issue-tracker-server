using StarWarsProgressBarIssueTracker.Domain.Releases;

namespace StarWarsProgressBarIssueTracker.App.Mutations;

public partial class IssueTrackerMutations
{
    public async Task<Release> AddRelease(string title, string? releaseNotes, DateTime? releaseDate)
    {
        return await releaseService.AddRelease(new()
        {
            Title = title,
            ReleaseNotes = releaseNotes,
            ReleaseDate = releaseDate,
            ReleaseState = ReleaseState.Open,
        });
    }

    public async Task<Release> UpdateRelease(Guid id, string title, ReleaseState state, string? releaseNotes, DateTime? releaseDate)
    {
        return await releaseService.UpdateRelease(new Release
        {
            Id = id,
            Title = title,
            ReleaseNotes = releaseNotes,
            ReleaseDate = releaseDate,
            ReleaseState = state
        });
    }

    public async Task<Release> DeleteRelease(Guid id)
    {
        return await releaseService.DeleteRelease(new Release
        {
            Id = id,
            Title = string.Empty
        });
    }
}
