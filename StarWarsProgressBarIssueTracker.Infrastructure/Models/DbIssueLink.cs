using System.ComponentModel.DataAnnotations.Schema;
using StarWarsProgressBarIssueTracker.Domain.Issues;

namespace StarWarsProgressBarIssueTracker.Infrastructure.Models;

public record DbIssueLink : DbEntityBase
{
    public LinkType Type { get; set; }

    [ForeignKey("LinkedIssueId")]
    public required DbIssue LinkedIssue { get; set; }
}
