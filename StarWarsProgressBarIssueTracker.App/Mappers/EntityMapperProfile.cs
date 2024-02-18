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
        CreateMap<Label, DbLabel>();
        CreateMap<Issue, DbIssue>();
        CreateMap<Milestone, DbMilestone>();
        CreateMap<DbLabel, Label>()
            .ForMember(dest => dest.GitlabId, options => options.Ignore())
            .ForMember(dest => dest.GitHubId, options => options.Ignore());
        CreateMap<DbIssue, Issue>()
            .ForMember(dest => dest.GitlabId, options => options.Ignore())
            .ForMember(dest => dest.GitlabIid, options => options.Ignore())
            .ForMember(dest => dest.GitHubId, options => options.Ignore());
        CreateMap<DbMilestone, Milestone>()
            .ForMember(dest => dest.GitlabId, options => options.Ignore())
            .ForMember(dest => dest.GitlabIid, options => options.Ignore())
            .ForMember(dest => dest.GitHubId, options => options.Ignore());

        CreateMap<Appearance, DbAppearance>().ReverseMap();
        CreateMap<IssueLink, DbIssueLink>().ReverseMap();
        CreateMap<Photo, DbPhoto>().ReverseMap();
        CreateMap<Release, DbRelease>().ReverseMap();
        CreateMap<Translation, DbTranslation>().ReverseMap();
        CreateMap<Vehicle, DbVehicle>().ReverseMap();
    }
}
