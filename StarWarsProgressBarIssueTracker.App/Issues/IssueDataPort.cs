using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StarWarsProgressBarIssueTracker.Domain;
using StarWarsProgressBarIssueTracker.Domain.Issues;
using StarWarsProgressBarIssueTracker.Infrastructure.Database;
using StarWarsProgressBarIssueTracker.Infrastructure.Models;
using StarWarsProgressBarIssueTracker.Infrastructure.Repositories;

namespace StarWarsProgressBarIssueTracker.App.Issues;

public class IssueDataPort : IDataPort<Issue>
{
    private readonly IIssueRepository _repository;
    private readonly IAppearanceRepository _appearanceRepository;
    private readonly IRepository<DbMilestone> _milestoneRepository;
    private readonly IRepository<DbRelease> _releaseRepository;
    private readonly IMapper _mapper;

    public IssueDataPort(IssueTrackerContext context,
        IIssueRepository repository,
        IAppearanceRepository appearanceRepository,
        IRepository<DbMilestone> milestoneRepository,
        IRepository<DbRelease> releaseRepository,
        IMapper mapper)
    {
        _repository = repository;
        _appearanceRepository = appearanceRepository;
        _milestoneRepository = milestoneRepository;
        _releaseRepository = releaseRepository;
        _repository.Context = context;
        _appearanceRepository.Context = context;
        _milestoneRepository.Context = context;
        _releaseRepository.Context = context;
        _mapper = mapper;
    }

    public async Task<Issue?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        DbIssue? dbIssue = await _repository.GetByIdAsync(id, cancellationToken);
        return _mapper.Map<Issue?>(dbIssue);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _repository.ExistsAsync(id, cancellationToken);
    }

    public async Task<IEnumerable<Issue>> GetAllAsync(CancellationToken cancellationToken)
    {
        List<DbIssue> dbIssues = await _repository.GetAll().ToListAsync(cancellationToken);

        return _mapper.Map<IEnumerable<Issue>>(dbIssues);
    }

    public async Task<Issue> AddAsync(Issue domain, CancellationToken cancellationToken = default)
    {
        var addedDbIssue = await _repository.AddAsync(_mapper.Map<DbIssue>(domain), cancellationToken);

        return _mapper.Map<Issue>(addedDbIssue);
    }

    public async Task AddRangeAsync(IEnumerable<Issue> domains, CancellationToken cancellationToken = default)
    {
        var dbIssues = _mapper.Map<IEnumerable<DbIssue>>(domains);
        await _repository.AddRangeAsync(dbIssues, cancellationToken);
    }

    public async Task<Issue> UpdateAsync(Issue domain, CancellationToken cancellationToken = default)
    {
        DbIssue dbIssue = (await _repository.GetByIdAsync(domain.Id, cancellationToken))!;

        dbIssue.Title = domain.Title;
        dbIssue.Description = domain.Description;
        dbIssue.State = domain.State;
        dbIssue.Priority = domain.Priority;

        await UpdateIssueMilestoneAsync(domain, dbIssue, cancellationToken);

        await UpdateIssueReleaseAsync(domain, dbIssue, cancellationToken);

        await UpdateIssueVehicleAsync(domain, dbIssue);

        await UpdateIssueLinksAsync(domain, dbIssue);

        DbIssue updatedIssue = await _repository.UpdateAsync(dbIssue, cancellationToken);

        return _mapper.Map<Issue>(updatedIssue);
    }

    private async Task UpdateIssueMilestoneAsync(Issue domain, DbIssue dbIssue, CancellationToken cancellationToken)
    {
        if (domain.Milestone?.Id != dbIssue.Milestone?.Id)
        {
            if (domain.Milestone == null)
            {
                dbIssue.Milestone = null;
            }
            else
            {
                DbMilestone? dbMilestone = await _milestoneRepository.GetByIdAsync(domain.Milestone.Id, cancellationToken);
                dbIssue.Milestone = dbMilestone;
            }
        }
    }

    private async Task UpdateIssueReleaseAsync(Issue domain, DbIssue dbIssue, CancellationToken cancellationToken)
    {
        if (domain.Release?.Id != dbIssue.Release?.Id)
        {
            if (domain.Release == null)
            {
                dbIssue.Release = null;
            }
            else
            {
                DbRelease? dbRelease = await _releaseRepository.GetByIdAsync(domain.Release.Id, cancellationToken);
                dbIssue.Release = dbRelease;
            }
        }
    }

    private async Task UpdateIssueVehicleAsync(Issue domain, DbIssue dbIssue)
    {
        if (domain.Vehicle == null)
        {
            if (dbIssue.Vehicle != null)
            {
                _repository.DeleteVehicle(dbIssue.Vehicle);
            }
            dbIssue.Vehicle = null;
        }
        else
        {
            if (dbIssue.Vehicle != null)
            {
                var dbVehicle = dbIssue.Vehicle;
                dbVehicle.EngineColor = domain.Vehicle.EngineColor;
                dbVehicle.Appearances =
                    await _appearanceRepository.GetAppearancesById(
                        domain.Vehicle.Appearances.Select(appearance => appearance.Id)).ToListAsync();

                UpdateTranslations(domain, dbIssue, dbVehicle);

                UpdatePhotos(domain, dbIssue, dbVehicle);
            }
            else
            {
                var dbVehicle = _mapper.Map<DbVehicle>(domain.Vehicle);
                dbVehicle.Appearances =
                    await _appearanceRepository.GetAppearancesById(
                        domain.Vehicle.Appearances.Select(appearance => appearance.Id)).ToListAsync();
                dbIssue.Vehicle = dbVehicle;
            }
        }
    }

    private void UpdateTranslations(Issue domain, DbIssue dbIssue, DbVehicle dbVehicle)
    {
        var addedTranslations = domain.Vehicle!.Translations.Where(translation =>
            !dbVehicle.Translations.Any(existingTranslation =>
                translation.Country.Equals(existingTranslation.Country)));

        var removedTranslations = dbVehicle.Translations.Where(existingTranslation =>
            !domain.Vehicle.Translations.Any(translation =>
                translation.Country.Equals(existingTranslation.Country)));

        var updatedTranslations = domain.Vehicle.Translations.Where(translation =>
                dbVehicle.Translations.Any(dbTranslation => translation.Country.Equals(dbTranslation.Country)))
            .ToDictionary(translation => translation.Country);

        var dbTranslationToUpdate = dbVehicle.Translations.Where(dbTranslation =>
                domain.Vehicle.Translations.Any(translation => dbTranslation.Country.Equals(translation.Country)))
            .ToList();

        _repository.DeleteTranslations(removedTranslations);

        foreach (var dbTranslation in dbTranslationToUpdate)
        {
            dbTranslation.Text = updatedTranslations[dbTranslation.Country].Text;
        }

        dbIssue.Vehicle!.Translations =
            dbTranslationToUpdate.Concat(_mapper.Map<IEnumerable<DbTranslation>>(addedTranslations)).ToList();
    }

    private void UpdatePhotos(Issue domain, DbIssue dbIssue, DbVehicle dbVehicle)
    {
        var addedPhotos = domain.Vehicle!.Photos.Where(photo =>
            !dbVehicle.Photos.Any(existingPhoto => photo.Id.Equals(existingPhoto.Id)));

        var removedPhotos = dbVehicle.Photos.Where(existingPhoto =>
            !domain.Vehicle.Photos.Any(photo => photo.Id.Equals(existingPhoto.Id)));

        var updatedPhotos = domain.Vehicle.Photos.Where(photo =>
                dbVehicle.Photos.Any(dbPhoto => photo.Id.Equals(dbPhoto.Id)))
            .ToDictionary(photo => photo.Id);

        var dbPhotosToUpdate = dbVehicle.Photos.Where(dbPhoto =>
            domain.Vehicle.Photos.Any(photo => dbPhoto.Id.Equals(photo.Id))).ToList();

        _repository.DeletePhotos(removedPhotos);

        foreach (var dbPhoto in dbPhotosToUpdate)
        {
            dbPhoto.FilePath = updatedPhotos[dbPhoto.Id].FilePath;
        }

        dbIssue.Vehicle!.Photos =
            dbPhotosToUpdate.Concat(_mapper.Map<IEnumerable<DbPhoto>>(addedPhotos)).ToList();
    }

    private async Task UpdateIssueLinksAsync(Issue domain, DbIssue dbIssue)
    {
        var issueLinks = domain.LinkedIssues;
        var dbIssueLinks = dbIssue.LinkedIssues;

        var addedLinks = issueLinks.Where(link =>
            !dbIssueLinks.Any(dbLink => link.Id.Equals(dbLink.Id)));

        var removedLinks = dbIssueLinks.Where(dbLink =>
            !issueLinks.Any(link => link.Id.Equals(dbLink.Id)));

        //TODO: add and remove links
        await Task.CompletedTask;
    }

    public async Task<Issue> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        DbIssue issue = (await _repository.GetByIdAsync(id, cancellationToken))!;

        issue.Milestone = null;
        issue.Vehicle?.Appearances.Clear();
        issue.Release = null;
        issue.Labels.Clear();
        // TODO: cascade delete only issue / vehicle related entities. Appearances, Milestones and Labels should stay.
        // Vehicle, Translations, Photos, Links should also be deleted

        return _mapper.Map<Issue>(await _repository.DeleteAsync(issue, cancellationToken));
    }

    public async Task DeleteRangeAsync(IEnumerable<Issue> domains, CancellationToken cancellationToken = default)
    {
        var issues = await _repository.GetAll().ToListAsync(cancellationToken);
        var toBeDeleted = issues.Where(dbIssue => domains.Any(issue => issue.Id.Equals(dbIssue.Id)));
        await _repository.DeleteRangeAsync(toBeDeleted, cancellationToken);
    }
}
