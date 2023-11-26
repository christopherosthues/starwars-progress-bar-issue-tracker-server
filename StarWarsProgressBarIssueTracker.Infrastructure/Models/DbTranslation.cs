using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StarWarsProgressBarIssueTracker.Infrastructure.Models;

[Table("Translations")]
public class DbTranslation
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [StringLength(7, MinimumLength = 2)]
    public required string Country { get; set; }

    [MaxLength(255)]
    public required string Text { get; set; }
}
