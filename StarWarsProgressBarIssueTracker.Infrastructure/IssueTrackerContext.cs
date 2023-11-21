using Microsoft.EntityFrameworkCore;
using StarWarsProgressBarIssueTracker.Infrastructure.Models;

namespace StarWarsProgressBarIssueTracker.Infrastructure;

public class IssueTrackerContext : DbContext
{
    public DbSet<DbLabel> Labels { get; set; } = default!;
    public DbSet<DbIssue> Issues { get; set; } = default!;
    public DbSet<DbMilestone> Milestones { get; set; } = default!;
    public DbSet<DbRelease> Releases { get; set; } = default!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(@"ConnectionString");
}
