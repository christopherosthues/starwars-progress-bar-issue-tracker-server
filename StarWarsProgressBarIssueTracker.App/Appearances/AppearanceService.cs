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
        return _mapper.Map<IEnumerable<Appearance>>(_repository.GetAll());
    }

    public Appearance GetAppearance(Guid id)
    {
        return _mapper.Map<Appearance>(_repository.GetById(id));
    }

    public void AddAppearance(Appearance appearance)
    {
        DbAppearance dbAppearance = new()
        {
            Title = appearance.Title,
            Description = appearance.Description,
            Color = appearance.Color,
            TextColor = appearance.TextColor
        };

        _repository.Add(dbAppearance);
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
