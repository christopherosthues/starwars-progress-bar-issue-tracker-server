using Microsoft.EntityFrameworkCore;
using StarWarsProgressBarIssueTracker.Infrastructure.Models;

namespace StarWarsProgressBarIssueTracker.Infrastructure.Database;

public class IssueTrackerContext(DbContextOptions<IssueTrackerContext> options) : DbContext(options)
{
    public DbSet<DbAppearance> Appearances { get; init; } = default!;
    public DbSet<DbIssue> Issues { get; init; } = default!;
    public DbSet<DbMilestone> Milestones { get; init; } = default!;
    public DbSet<DbRelease> Releases { get; init; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DbAppearance>().ToTable(nameof(Appearances));
        modelBuilder.Entity<DbIssue>().ToTable(nameof(Issues));
        modelBuilder.Entity<DbMilestone>().ToTable(nameof(Milestones));
        modelBuilder.Entity<DbRelease>().ToTable(nameof(Releases));
    }
}
