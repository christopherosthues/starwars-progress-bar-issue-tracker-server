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

    public async Task<IEnumerable<Appearance>> GetAllAppearances()
    {
        return _mapper.Map<IEnumerable<Appearance>>(await _repository.GetAll());
    }

    public async Task<Appearance> GetAppearance(Guid id)
    {
        return _mapper.Map<Appearance>(await _repository.GetById(id));
    }

    public async Task<Appearance> AddAppearance(string title, string color, string textColor, string? description)
    {
        DbAppearance dbAppearance = new()
        {
            Title = title,
            Description = description,
            Color = color,
            TextColor = textColor
        };

        return _mapper.Map<Appearance>(await _repository.Add(dbAppearance));
    }

    public Appearance UpdateAppearance(Appearance appearance)
    {
        throw new NotImplementedException();
    }

    public Appearance DeleteAppearance(Appearance appearance)
    {
        throw new NotImplementedException();
    }
}
