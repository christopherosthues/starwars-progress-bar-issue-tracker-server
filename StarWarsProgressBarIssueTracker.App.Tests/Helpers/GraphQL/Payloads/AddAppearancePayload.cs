using StarWarsProgressBarIssueTracker.Domain.Appearances;

namespace StarWarsProgressBarIssueTracker.App.Tests.Helpers.GraphQL.Payloads;

public class AddAppearancePayload
{
    public required Appearance Appearance { get; set; }
}
