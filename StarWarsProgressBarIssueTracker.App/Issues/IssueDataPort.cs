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
    private readonly IRepository<DbMilestone> _milestoneRepository;
    private readonly IRepository<DbRelease> _releaseRepository;
    private readonly IMapper _mapper;

    public IssueDataPort(IssueTrackerContext context,
        IIssueRepository repository,
        IRepository<DbMilestone> milestoneRepository,
        IRepository<DbRelease> releaseRepository,
        IMapper mapper)
    {
        _repository = repository;
        _milestoneRepository = milestoneRepository;
        _releaseRepository = releaseRepository;
        _repository.Context = context;
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

        await UpdateIssueMilestoneAsync(domain, cancellationToken, dbIssue);

        await UpdateIssueReleaseAsync(domain, cancellationToken, dbIssue);

        UpdateIssueVehicle(domain, dbIssue);

        // TODO: update linked issues.

        DbIssue updatedIssue = await _repository.UpdateAsync(dbIssue, cancellationToken);

        return _mapper.Map<Issue>(updatedIssue);
    }

    private async Task UpdateIssueMilestoneAsync(Issue domain, CancellationToken cancellationToken, DbIssue dbIssue)
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

    private async Task UpdateIssueReleaseAsync(Issue domain, CancellationToken cancellationToken, DbIssue dbIssue)
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

    private void UpdateIssueVehicle(Issue domain, DbIssue dbIssue)
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
            // TODO: update vehicle and dependencies
        }
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
