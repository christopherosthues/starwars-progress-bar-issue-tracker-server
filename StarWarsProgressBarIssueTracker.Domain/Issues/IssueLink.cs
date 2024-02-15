using StarWarsProgressBarIssueTracker.Domain.Models;

namespace StarWarsProgressBarIssueTracker.Domain.Issues;

public class IssueLink : DomainBase
{
    public LinkType Type { get; set; }

    public required Issue IncomingLink { get; set; }

    public required Issue OutgoingLink { get; set; }
}
