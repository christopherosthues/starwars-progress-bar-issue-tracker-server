using AutoMapper;
using StarWarsProgressBarIssueTracker.Domain.Exceptions;
using StarWarsProgressBarIssueTracker.Domain.Labels;
using StarWarsProgressBarIssueTracker.Domain.Vehicles;
using StarWarsProgressBarIssueTracker.Infrastructure.Database;
using StarWarsProgressBarIssueTracker.Infrastructure.Models;

namespace StarWarsProgressBarIssueTracker.Infrastructure.Repositories;

public class LabelRepository(IssueTrackerContext context, IMapper mapper)
    : IssueTrackerRepositoryBase<Label, DbLabel>(context, mapper), ILabelRepository
{
    protected override async Task<DbLabel> Map(Label domain, bool add = false, bool update = false)
    {
        if (add)
        {
            return new DbLabel
            {
                Title = domain.Title,
                Description = domain.Description,
                Color = domain.Color,
                TextColor = domain.TextColor
            };
        }

        var dbAppearance = await DbSet.FindAsync(domain.Id) ?? throw new DomainIdNotFoundException(nameof(Label), domain.Id.ToString());

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

    protected override void DeleteRelationships(DbLabel entity)
    {
        var dbVehicles = Context.Vehicles.Where(dbVehicle =>
            dbVehicle.Appearances.Any(appearance => appearance.Id.Equals(entity.Id)));

        foreach (var dbVehicle in dbVehicles)
        {
            dbVehicle.Appearances.Remove(entity);
        }
    }
}
