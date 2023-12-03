namespace StarWarsProgressBarIssueTracker.Infrastructure.Models;

public class DbPhoto : DbEntityBase
{
    public required byte[] PhotoData { get; set; }
}
