using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StarWarsProgressBarIssueTracker.Domain.Exceptions;
using StarWarsProgressBarIssueTracker.Domain.Issues;
using StarWarsProgressBarIssueTracker.Infrastructure.Database;
using StarWarsProgressBarIssueTracker.Infrastructure.Models;

namespace StarWarsProgressBarIssueTracker.Infrastructure.Repositories;

public class IssueRepository(IssueTrackerContext context, IMapper mapper)
    : IssueTrackerRepositoryBase<Issue, DbIssue>(context, mapper), IIssueRepository
{
    protected override IQueryable<DbIssue> GetIncludingFields()
    {
        return Context.Issues.Include(dbIssue => dbIssue.Milestone)
            .Include(dbIssue => dbIssue.Release)
            .Include(dbIssue => dbIssue.Vehicle)
            .ThenInclude(dbVehicle => dbVehicle != null ? dbVehicle.Appearances : null)
            .Include(dbIssue => dbIssue.Vehicle)
            .ThenInclude(dbVehicle => dbVehicle != null ? dbVehicle.Photos : null)
            .Include(dbIssue => dbIssue.Vehicle)
            .ThenInclude(dbVehicle => dbVehicle != null ? dbVehicle.Translations : null);
    }

    protected override async Task<DbIssue> Map(Issue domain, bool add = false, bool update = false)
    {
        DbMilestone? dbMilestone = null;
        if (domain.Milestone != null)
        {
            dbMilestone = await Context.Milestones.Include(dbMilestone1 => dbMilestone1.Issues)
                .FirstOrDefaultAsync(dbMilestone1 => dbMilestone1.Id.Equals(domain.Milestone.Id));
        }
        DbRelease? dbRelease = null;
        if (domain.Release != null)
        {
            dbRelease = await Context.Releases.Include(dbRelease1 => dbRelease1.Issues)
                .FirstOrDefaultAsync(dbRelease1 => dbRelease1.Id.Equals(domain.Release.Id));
        }

        var vehicle = domain.Vehicle;
        if (add)
        {
            return AddIssue(domain, vehicle, dbMilestone, dbRelease);
        }

        var dbIssue = await GetIncludingFields().SingleOrDefaultAsync(dbIssue => dbIssue.Id.Equals(domain.Id)) ?? throw new DomainIdNotFoundException(nameof(Issue), domain.Id.ToString());

        if (update)
        {
            await UpdateIssue(domain, vehicle, dbIssue, dbMilestone, dbRelease);
        }

        return dbIssue;
    }

    private DbIssue AddIssue(Issue domain, Vehicle? vehicle, DbMilestone? dbMilestone, DbRelease? dbRelease)
    {
        DbVehicle? dbVehicle = null;
        if (vehicle is not null)
        {
            var dbAppearances = Context.Appearances.AsEnumerable().Where(dbAppearance =>
                vehicle.Appearances.Any(appearance => appearance.Id.Equals(dbAppearance.Id))).ToList();
            dbVehicle = new DbVehicle
            {
                EngineColor = vehicle.EngineColor,
                Photos = vehicle.Photos.Select(photo => new DbPhoto
                {
                    PhotoData = Convert.FromBase64String(photo.PhotoData)
                }).ToList(),
                Translations = vehicle.Translations.Select(translation => new DbTranslation
                {
                    Country = translation.Country,
                    Text = translation.Text
                }).ToList(),
                Appearances = dbAppearances
            };
        }

        var newIssue = new DbIssue
        {
            Title = domain.Title,
            Description = domain.Description,
            IssueState = domain.IssueState,
            Milestone = dbMilestone,
            Release = dbRelease,
            Vehicle = dbVehicle,
            Priority = domain.Priority,
            IssueType = domain.IssueType,
        };

        dbMilestone?.Issues.Add(newIssue);
        dbRelease?.Issues.Add(newIssue);

        return newIssue;
    }

    private async Task UpdateIssue(Issue domain, Vehicle? vehicle, DbIssue dbIssue, DbMilestone? dbMilestone,
        DbRelease? dbRelease)
    {
        var dbVehicle = await Context.Vehicles.Include(dbVehicle => dbVehicle.Photos)
            .Include(dbVehicle => dbVehicle.Translations)
            .FirstOrDefaultAsync(dbVehicle => dbVehicle.Id.Equals(vehicle != null ? vehicle.Id : null));
        if (vehicle is not null)
        {
            var dbAppearances = Context.Appearances.AsEnumerable().Where(dbAppearance =>
                vehicle.Appearances.Any(appearance => appearance.Id.Equals(dbAppearance.Id))).ToList();

            IEnumerable<DbPhoto> addedPhotos = new List<DbPhoto>();
            IEnumerable<DbTranslation> addedTranslations = new List<DbTranslation>();
            if (dbVehicle is not null)
            {
                var dbVehiclePhotos = dbVehicle.Photos;
                addedPhotos = vehicle.Photos.Where(photo => !dbVehiclePhotos.Any(dbPhoto => photo.Id.Equals(dbPhoto.Id)))
                    .Select(photo => new DbPhoto
                    {
                        PhotoData = Convert.FromBase64String(photo.PhotoData)
                    });

                var dbVehicleTranslations = dbVehicle.Translations;
                addedTranslations = vehicle.Translations.Where(translation => !dbVehicleTranslations.Any(dbTranslation =>
                        translation.Id.Equals(dbTranslation.Id)))
                    .Select(translation => new DbTranslation
                    {
                        Country = translation.Country,
                        Text = translation.Text,
                    });
            }

            var changedPhotos = dbVehicle?.Photos
                .Where(dbPhoto => vehicle.Photos.Any(photo => photo.Id.Equals(dbPhoto.Id))).ToList() ?? [];

            foreach (var changedPhoto in changedPhotos)
            {
                changedPhoto.PhotoData = Convert.FromBase64String(vehicle.Photos.First(photo => photo.Id.Equals(changedPhoto.Id)).PhotoData);
            }

            var changedTranslations = dbVehicle?.Translations.Where(dbTranslation =>
                vehicle.Translations.Any(translation => translation.Id.Equals(dbTranslation.Id))).ToList() ?? [];

            foreach (var changedTranslation in changedTranslations)
            {
                var translation = vehicle.Translations.First(translation => translation.Id.Equals(changedTranslation.Id));
                changedTranslation.Country = translation.Country;
                changedTranslation.Text = translation.Text;
            }

            dbVehicle ??= new DbVehicle();
            dbVehicle.EngineColor = vehicle.EngineColor;
            dbVehicle.Photos = addedPhotos.Concat(changedPhotos).ToList();
            dbVehicle.Translations = addedTranslations.Concat(changedTranslations).ToList();
            dbVehicle.Appearances = dbAppearances;
        }

        dbIssue.Title = domain.Title;
        dbIssue.Description = domain.Description;
        dbIssue.IssueState = domain.IssueState;
        dbIssue.Milestone = dbMilestone;
        dbIssue.Release = dbRelease;
        dbIssue.Vehicle = dbVehicle;
        dbIssue.Priority = domain.Priority;
        dbIssue.IssueType = domain.IssueType;
        dbIssue.LastModifiedAt = DateTime.UtcNow;

        var milestoneIssue = dbMilestone?.Issues.FirstOrDefault(issue => issue.Id.Equals(dbIssue.Id));
        if (milestoneIssue is not null)
        {
            dbMilestone?.Issues.Remove(milestoneIssue);
        }
        dbMilestone?.Issues.Add(dbIssue);

        var releaseIssue = dbRelease?.Issues.FirstOrDefault(issue => issue.Id.Equals(dbIssue.Id));
        if (releaseIssue is not null)
        {
            dbRelease?.Issues.Remove(releaseIssue);
        }
        dbRelease?.Issues.Add(dbIssue);
    }

    protected override void DeleteRelationships(DbIssue entity)
    {
        entity.Milestone?.Issues.Remove(entity);
        entity.Release?.Issues.Remove(entity);

        if (entity.Vehicle is not null)
        {
            Context.Vehicles.Remove(entity.Vehicle);
        }
    }
}
