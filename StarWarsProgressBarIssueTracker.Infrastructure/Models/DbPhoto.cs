using System.ComponentModel.DataAnnotations.Schema;

namespace StarWarsProgressBarIssueTracker.Infrastructure.Models;

[Table("Photos")]
public class DbPhoto : DbEntityBase
{
    public required byte[] PhotoData { get; set; }
}
