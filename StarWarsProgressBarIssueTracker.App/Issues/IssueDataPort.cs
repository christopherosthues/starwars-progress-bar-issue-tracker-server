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
    private readonly IRepository<DbIssue> _repository;
    private readonly IMapper _mapper;

    public IssueDataPort(IssueTrackerContext context, IRepository<DbIssue> repository, IMapper mapper)
    {
        _repository = repository;
        _repository.Context = context;
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

    public async Task<Issue> UpdateAsync(Issue domain, CancellationToken cancellationToken = default)
    {
        DbIssue deIssue = (await _repository.GetByIdAsync(domain.Id, cancellationToken))!;

        deIssue.Title = domain.Title;
        deIssue.Description = domain.Description;
        deIssue.State = domain.State;
        deIssue.Priority = domain.Priority;
        // TODO: load milestone, release, vehicle and linked issues. Update them all

        DbIssue updatedIssue = await _repository.UpdateAsync(deIssue, cancellationToken);

        return _mapper.Map<Issue>(updatedIssue);
    }

    public async Task<Issue> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        DbIssue issue = (await _repository.GetByIdAsync(id, cancellationToken))!;

        return _mapper.Map<Issue>(await _repository.DeleteAsync(issue, cancellationToken));
    }
}
