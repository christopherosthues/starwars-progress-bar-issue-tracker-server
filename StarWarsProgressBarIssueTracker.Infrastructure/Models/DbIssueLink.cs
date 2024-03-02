using Microsoft.EntityFrameworkCore;
using StarWarsProgressBarIssueTracker.Domain.Issues;

namespace StarWarsProgressBarIssueTracker.Infrastructure.Models;

public record DbIssueLink : DbEntityBase
{
    public LinkType Type { get; set; }

    [DeleteBehavior(DeleteBehavior.SetNull)]
    public required DbIssue LinkedIssue { get; set; }
}
