using AutoMapper;
using StarWarsProgressBarIssueTracker.Domain;
using StarWarsProgressBarIssueTracker.Domain.Appearances;
using StarWarsProgressBarIssueTracker.Infrastructure.Models;

namespace StarWarsProgressBarIssueTracker.App.Appearances;

public class AppearanceService : IAppearanceService
{
    private readonly IRepository<DbAppearance> _repository;
    private readonly IMapper _mapper;

    public AppearanceService(IRepository<DbAppearance> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public IEnumerable<Appearance> GetAllAppearances()
    {
        return _mapper.Map<IEnumerable<DbAppearance>, IEnumerable<Appearance>>(_repository.GetAll());
    }

    public Appearance GetAppearance(Guid id)
    {
        throw new NotImplementedException();
    }

    public void AddAppearance(Appearance appearance)
    {
        throw new NotImplementedException();
    }

    public void UpdateAppearance(Appearance appearance)
    {
        throw new NotImplementedException();
    }

    public void DeleteAppearance(Appearance appearance)
    {
        throw new NotImplementedException();
    }
}
