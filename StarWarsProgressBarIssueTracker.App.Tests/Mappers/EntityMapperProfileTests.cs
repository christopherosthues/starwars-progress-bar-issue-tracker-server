using AutoMapper;
using StarWarsProgressBarIssueTracker.App.Mappers;

namespace StarWarsProgressBarIssueTracker.App.Tests.Mappers;

[TestFixture(TestOf = typeof(EntityMapperProfile))]
public class EntityMapperProfileTests
{
    [Test]
    public void MapperShouldBeConfiguredCorrectly()
    {
        // Arrange
        var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<EntityMapperProfile>());

        // Act and Assert
        mapperConfiguration.AssertConfigurationIsValid();
    }
}
