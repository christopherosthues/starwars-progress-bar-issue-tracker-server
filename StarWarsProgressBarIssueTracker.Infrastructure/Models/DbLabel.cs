namespace StarWarsProgressBarIssueTracker.Infrastructure.Models;

public class DbLabel : DbEntity
{
    public required string Title { get; set; }
    
    public string? Description { get; set; }
    
    public required string Color { get; set; }
    
    public required string TextColor { get; set; }
}
