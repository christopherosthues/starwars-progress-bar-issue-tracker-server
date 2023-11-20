using HotChocolate;
using StarWarsProgressBarIssueTracker.Domain.Milestones;

namespace StarWarsProgressBarIssueTracker.App.Tests.Helpers.GraphQL.Payloads.Milestones;

public class AddMilestonePayload
{
    public required Milestone Milestone { get; set; }

    public Error[] Errors { get; set; } = [];
}
