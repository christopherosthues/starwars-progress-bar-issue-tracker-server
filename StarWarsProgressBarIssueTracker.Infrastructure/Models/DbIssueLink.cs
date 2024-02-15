using StarWarsProgressBarIssueTracker.Domain.Issues;

namespace StarWarsProgressBarIssueTracker.Infrastructure.Models;

public record DbIssueLink : DbEntityBase
{
    public LinkType Type { get; set; }

    public required DbIssue IncomingLink { get; set; }

    public required DbIssue OutgoingLink { get; set; }
}
