using Microsoft.EntityFrameworkCore;
using StarWarsProgressBarIssueTracker.Infrastructure.Models;

namespace StarWarsProgressBarIssueTracker.Infrastructure;

public class IssueTrackerContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<DbLabel> Labels { get; init; } = default!;
    public DbSet<DbIssue> Issues { get; init; } = default!;
    public DbSet<DbMilestone> Milestones { get; init; } = default!;
    public DbSet<DbRelease> Releases { get; init; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DbLabel>().ToTable(nameof(Labels));
        modelBuilder.Entity<DbIssue>().ToTable(nameof(Issues));
        modelBuilder.Entity<DbMilestone>().ToTable(nameof(Milestones));
        modelBuilder.Entity<DbRelease>().ToTable(nameof(Releases));
    }
}
