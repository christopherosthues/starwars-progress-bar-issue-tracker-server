using System.ComponentModel.DataAnnotations.Schema;

namespace StarWarsProgressBarIssueTracker.Infrastructure.Models;

[Table("Translations")]
public record DbTranslation
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Country { get; set; }
    public required string Text { get; set; }
}
