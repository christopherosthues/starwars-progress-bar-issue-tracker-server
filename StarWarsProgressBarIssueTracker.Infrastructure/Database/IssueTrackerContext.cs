using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StarWarsProgressBarIssueTracker.Infrastructure.Models;

namespace StarWarsProgressBarIssueTracker.Infrastructure.Database;

public class IssueTrackerContext(DbContextOptions<IssueTrackerContext> options, IOptions<IssueTrackerDbConfig> configuration) : DbContext(options)
{
    private readonly IssueTrackerDbConfig _configuration = configuration.Value;

    public DbSet<DbAppearance> Appearances { get; init; } = default!;
    public DbSet<DbIssue> Issues { get; init; } = default!;
    public DbSet<DbIssueLink> IssueLinks { get; init; } = default!;
    public DbSet<DbLabel> Labels { get; init; } = default!;
    public DbSet<DbMilestone> Milestones { get; init; } = default!;
    public DbSet<DbRelease> Releases { get; init; } = default!;
    public DbSet<DbVehicle> Vehicles { get; init; } = default!;
    public DbSet<DbPhoto> Photos { get; init; } = default!;
    public DbSet<DbTranslation> Translations { get; init; } = default!;
    public DbSet<DbJob> Jobs { get; init; } = default!;
    public DbSet<DbTask> Tasks { get; init; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DbAppearance>().ToTable(nameof(Appearances), _configuration.Schema);
        modelBuilder.Entity<DbIssue>().ToTable(nameof(Issues), _configuration.Schema);
        modelBuilder.Entity<DbIssueLink>().ToTable(nameof(IssueLinks), _configuration.Schema);
        modelBuilder.Entity<DbLabel>().ToTable(nameof(Labels), _configuration.Schema);
        modelBuilder.Entity<DbMilestone>().ToTable(nameof(Milestones), _configuration.Schema);
        modelBuilder.Entity<DbRelease>().ToTable(nameof(Releases), _configuration.Schema);
        modelBuilder.Entity<DbVehicle>().ToTable(nameof(Vehicles), _configuration.Schema);
        modelBuilder.Entity<DbPhoto>().ToTable(nameof(Photos), _configuration.Schema);
        modelBuilder.Entity<DbTranslation>().ToTable(nameof(Translations), _configuration.Schema);
        modelBuilder.Entity<DbJob>().ToTable(nameof(Jobs), _configuration.Schema);
        modelBuilder.Entity<DbTask>().ToTable(nameof(Tasks), _configuration.Schema);

        modelBuilder.Entity<DbIssue>()
            .HasOne<DbVehicle>();
    }
}
