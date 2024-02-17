using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StarWarsProgressBarIssueTracker.Domain;
using StarWarsProgressBarIssueTracker.Domain.Releases;
using StarWarsProgressBarIssueTracker.Infrastructure.Database;
using StarWarsProgressBarIssueTracker.Infrastructure.Models;
using StarWarsProgressBarIssueTracker.Infrastructure.Repositories;

namespace StarWarsProgressBarIssueTracker.App.Releases;

public class ReleaseDataPort : IDataPort<Release>
{
    private readonly IRepository<DbRelease> _repository;
    private readonly IMapper _mapper;

    public ReleaseDataPort(IssueTrackerContext context, IRepository<DbRelease> repository, IMapper mapper)
    {
        _repository = repository;
        _repository.Context = context;
        _mapper = mapper;
    }

    public async Task<Release?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        DbRelease? dbRelease = await _repository.GetByIdAsync(id, cancellationToken);
        return _mapper.Map<Release?>(dbRelease);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _repository.ExistsAsync(id, cancellationToken);
    }

    public async Task<IEnumerable<Release>> GetAllAsync(CancellationToken cancellationToken)
    {
        List<DbRelease> dbReleases = await _repository.GetAll().ToListAsync(cancellationToken);

        return _mapper.Map<IEnumerable<Release>>(dbReleases);
    }

    public async Task<Release> AddAsync(Release domain, CancellationToken cancellationToken = default)
    {
        var addedDbRelease = await _repository.AddAsync(_mapper.Map<DbRelease>(domain), cancellationToken);

        return _mapper.Map<Release>(addedDbRelease);
    }

    public async Task<Release> UpdateAsync(Release domain, CancellationToken cancellationToken = default)
    {
        DbRelease deRelease = (await _repository.GetByIdAsync(domain.Id, cancellationToken))!;

        deRelease.Title = domain.Title;
        deRelease.Notes = domain.Notes;
        deRelease.Date = domain.Date;
        deRelease.State = domain.State;

        DbRelease updatedRelease = await _repository.UpdateAsync(deRelease, cancellationToken);

        return _mapper.Map<Release>(updatedRelease);
    }

    public async Task<Release> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        DbRelease release = (await _repository.GetByIdAsync(id, cancellationToken))!;

        return _mapper.Map<Release>(await _repository.DeleteAsync(release, cancellationToken));
    }
}
