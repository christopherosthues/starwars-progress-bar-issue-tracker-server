using StarWarsProgressBarIssueTracker.Domain.Appearances;

namespace StarWarsProgressBarIssueTracker.App.Tests.Helpers.GraphQL.Payloads;

public class GetAppearancesResponse
{
    public IEnumerable<Appearance> Appearances { get; set; } = [];
}
