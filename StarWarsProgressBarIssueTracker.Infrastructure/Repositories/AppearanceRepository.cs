using AutoMapper;
using StarWarsProgressBarIssueTracker.Domain.Appearances;
using StarWarsProgressBarIssueTracker.Domain.Exceptions;
using StarWarsProgressBarIssueTracker.Infrastructure.Database;
using StarWarsProgressBarIssueTracker.Infrastructure.Models;

namespace StarWarsProgressBarIssueTracker.Infrastructure.Repositories;

public class AppearanceRepository(IssueTrackerContext context, IMapper mapper)
    : IssueTrackerRepositoryBase<Appearance, DbAppearance>(context, mapper), IAppearanceRepository
{
    protected override async Task<DbAppearance> Map(Appearance domain, bool add = false, bool update = false)
    {
        if (add)
        {
            return new DbAppearance
            {
                Title = domain.Title,
                Description = domain.Description,
                Color = domain.Color,
                TextColor = domain.TextColor
            };
        }

        var dbAppearance = await DbSet.FindAsync(domain.Id);

        if (dbAppearance is null)
        {
            throw new DomainIdNotFoundException(nameof(Appearance), domain.Id.ToString());
        }

        if (update)
        {
            dbAppearance.Title = domain.Title;
            dbAppearance.Description = domain.Description;
            dbAppearance.Color = domain.Color;
            dbAppearance.TextColor = domain.TextColor;
            dbAppearance.LastModifiedAt = DateTime.UtcNow;
        }

        return dbAppearance;
    }

    protected override void DeleteRelationships(DbAppearance entity)
    {
        var dbVehicles = Context.Vehicles.Where(dbVehicle =>
            dbVehicle.Appearances.Any(appearance => appearance.Id.Equals(entity.Id)));

        foreach (var dbVehicle in dbVehicles)
        {
            dbVehicle.Appearances.Remove(entity);
        }
    }
}
