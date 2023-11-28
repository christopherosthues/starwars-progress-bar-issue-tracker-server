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
        CreateMap<Appearance, DbAppearance>();
        CreateMap<Issue, DbIssue>();
        CreateMap<Photo, DbPhoto>()
            .ForMember(dest => dest.PhotoData, opt => opt.MapFrom(src => src.PhotoData));
        CreateMap<Translation, DbTranslation>();
        CreateMap<Vehicle, DbVehicle>();
        CreateMap<Milestone, DbMilestone>();
        CreateMap<Release, DbRelease>();
    }
}
