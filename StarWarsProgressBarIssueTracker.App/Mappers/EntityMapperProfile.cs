using AutoMapper;
using StarWarsProgressBarIssueTracker.App.Mappers.Converters;
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
            .ForMember(dest => dest.PhotoData, opt => opt.ConvertUsing(new PhotoConverter(), src => src.PhotoData));
        CreateMap<Translation, DbTranslation>().ReverseMap();
        CreateMap<Vehicle, DbVehicle>()
            .ReverseMap();
        CreateMap<Milestone, DbMilestone>().ReverseMap();
        CreateMap<Release, DbRelease>().ReverseMap();
        CreateMap<DbPhoto, Photo>()
            .ForMember(dest => dest.PhotoData, opt => opt.ConvertUsing(new DbPhotoConverter(), src => src.PhotoData));

        CreateMap<DbJob, DbJob>();
        CreateMap<DbTask, DbTask>();
    }
}
