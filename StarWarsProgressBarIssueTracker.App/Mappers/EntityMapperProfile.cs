using AutoMapper;
using StarWarsProgressBarIssueTracker.Domain.Appearances;
using StarWarsProgressBarIssueTracker.Domain.Issues;
using StarWarsProgressBarIssueTracker.Domain.Milestones;
using StarWarsProgressBarIssueTracker.Domain.Releases;
using StarWarsProgressBarIssueTracker.Infrastructure.Models;

namespace StarWarsProgressBarIssueTracker.App.Mappers;

public class EntityMapperProfile : Profile
{
    public EntityMapperProfile()
    {
        CreateMap<Appearance, DbAppearance>().ReverseMap();
        CreateMap<Issue, DbIssue>().ReverseMap();
        CreateMap<Photo, DbPhoto>()
            .ForMember(dest => dest.PhotoData, opt => opt.MapFrom(src => src.PhotoData))
            .ReverseMap();
        CreateMap<Translation, DbTranslation>().ReverseMap();
        CreateMap<Vehicle, DbVehicle>().ReverseMap();
        CreateMap<Milestone, DbMilestone>().ReverseMap();
        CreateMap<Release, DbRelease>().ReverseMap();
    }
}
