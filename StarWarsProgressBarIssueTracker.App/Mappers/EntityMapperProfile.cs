using AutoMapper;
using StarWarsProgressBarIssueTracker.Domain.Issues;
using StarWarsProgressBarIssueTracker.Domain.Labels;
using StarWarsProgressBarIssueTracker.Domain.Milestones;
using StarWarsProgressBarIssueTracker.Domain.Releases;
using StarWarsProgressBarIssueTracker.Domain.Vehicles;
using StarWarsProgressBarIssueTracker.Infrastructure.Models;

namespace StarWarsProgressBarIssueTracker.App.Mappers;

public class EntityMapperProfile : Profile
{
    public EntityMapperProfile()
    {
        CreateMap<Appearance, DbAppearance>().ReverseMap();
        CreateMap<Issue, DbIssue>().ReverseMap();
        CreateMap<IssueLink, DbIssueLink>().ReverseMap();
        CreateMap<Label, DbLabel>().ReverseMap();
        CreateMap<Milestone, DbMilestone>().ReverseMap();
        CreateMap<Photo, DbPhoto>().ReverseMap();
        CreateMap<Release, DbRelease>().ReverseMap();
        CreateMap<Translation, DbTranslation>().ReverseMap();
        CreateMap<Vehicle, DbVehicle>().ReverseMap();
    }
}
