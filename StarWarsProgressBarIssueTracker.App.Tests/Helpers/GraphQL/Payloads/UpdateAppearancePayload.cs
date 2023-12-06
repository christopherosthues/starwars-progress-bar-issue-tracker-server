using HotChocolate;
using StarWarsProgressBarIssueTracker.Domain.Appearances;

namespace StarWarsProgressBarIssueTracker.App.Tests.Helpers.GraphQL.Payloads;

public class UpdateAppearancePayload
{
    public required Appearance Appearance { get; set; }

    public Error[] Errors { get; set; } = [];
}
