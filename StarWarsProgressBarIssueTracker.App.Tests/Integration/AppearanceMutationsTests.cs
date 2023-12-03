using Bogus;
using FluentAssertions;
using FluentAssertions.Execution;
using GraphQL;
using StarWarsProgressBarIssueTracker.App.Mutations;
using StarWarsProgressBarIssueTracker.App.Tests.Helpers.GraphQL.Payloads;
using StarWarsProgressBarIssueTracker.Domain.Appearances;
using StarWarsProgressBarIssueTracker.Infrastructure.Models;

namespace StarWarsProgressBarIssueTracker.App.Tests.Integration;

[TestFixture(TestOf = typeof(IssueTrackerMutations))]
public class AppearanceMutationsTests : IntegrationTestBase
{
    private const string AllowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789ß_#%";
    private const string HexcodeColorChars = "0123456789abcdef";

    [TestCaseSource(nameof(AddAppearanceCases))]
    public async Task AddAppearanceShouldAddAppearance(Appearance expectedAppearance)
    {
        // Arrange
        CheckDbContent(context =>
        {
            context.Appearances.Should().BeEmpty();
        });
        var mutationRequest = CreateAddRequest(expectedAppearance);

        var startTime = DateTime.UtcNow;

        // Act
        var response = await GraphQLClient.SendMutationAsync<AddAppearanceResponse>(mutationRequest);

        // Assert
        AssertAddedAppearance(response, expectedAppearance, startTime);
    }

    [TestCaseSource(nameof(AddAppearanceCases))]
    public async Task AddAppearanceShouldAddAppearanceIfAppearancesAreNotEmpty(Appearance expectedAppearance)
    {
        // Arrange
        var dbAppearance = new DbAppearance
        {
            Id = new Guid("87653DC5-B029-4BA6-959A-1FBFC48E2C81"),
            Title = "Title",
            Description = "Desc",
            Color = "001122",
            TextColor = "334455",
            CreatedAt = DateTime.UtcNow.AddDays(-2),
            LastModifiedAt = DateTime.UtcNow.AddDays(-1)
        };
        await SeedDatabase(context =>
        {
            context.Appearances.Add(dbAppearance);
        });
        CheckDbContent(context =>
        {
            context.Appearances.Should().NotBeEmpty();
        });
        var mutationRequest = CreateAddRequest(expectedAppearance);

        var startTime = DateTime.UtcNow;

        // Act
        var response = await GraphQLClient.SendMutationAsync<AddAppearanceResponse>(mutationRequest);

        // Assert
        AssertAddedAppearance(response, expectedAppearance, startTime, dbAppearance);
    }

    [TestCaseSource(nameof(InvalidAddAppearanceCases))]
    public async Task AddAppearanceShouldNotAddAppearance(Appearance expectedAppearance)
    {
        // Arrange
        CheckDbContent(context =>
        {
            context.Appearances.Should().BeEmpty();
        });
        var mutationRequest = CreateAddRequest(expectedAppearance);

        // Act
        var response = await GraphQLClient.SendMutationAsync<AddAppearanceResponse>(mutationRequest);

        // Assert
        AssertAppearanceNotAdded(response);
    }

    private static GraphQLRequest CreateAddRequest(Appearance expectedAppearance)
    {
        var descriptionParameter = expectedAppearance.Description != null
            ? $"""
               , description: "{expectedAppearance.Description}"
               """
            : string.Empty;
        var mutationRequest = new GraphQLRequest
        {
            Query = $$"""
                      mutation addAppearance
                      {
                          addAppearance(input: {title: "{{expectedAppearance.Title}}", color: "{{expectedAppearance.Color}}", textColor: "{{expectedAppearance.TextColor}}"{{descriptionParameter}}})
                          {
                              appearance
                              {
                                  id
                                  title
                                  description
                                  color
                                  textColor
                                  createdAt
                                  lastModifiedAt
                              }
                          }
                      }
                      """,
            OperationName = "addAppearance"
        };
        return mutationRequest;
    }

    private void AssertAddedAppearance(GraphQLResponse<AddAppearanceResponse> response, Appearance expectedAppearance,
        DateTime startTime, DbAppearance? dbAppearance = null)
    {
        DateTime endTime = DateTime.UtcNow;
        Appearance? addedAppearance;
        using (new AssertionScope())
        {
            response.Should().NotBeNull();
            response.Errors.Should().BeNullOrEmpty();
            addedAppearance = response.Data.AddAppearance.Appearance;
            addedAppearance.Id.Should().NotBeEmpty();
            addedAppearance.Title.Should().Be(expectedAppearance.Title);
            addedAppearance.Description.Should().Be(expectedAppearance.Description);
            addedAppearance.Color.Should().Be(expectedAppearance.Color);
            addedAppearance.TextColor.Should().Be(expectedAppearance.TextColor);
            addedAppearance.CreatedAt.Should().BeCloseTo(startTime, TimeSpan.FromMilliseconds(300), "Start time").And
                .BeCloseTo(endTime, TimeSpan.FromMilliseconds(300), "End time");
            addedAppearance.LastModifiedAt.Should().BeNull();
        }

        CheckDbContent(context =>
        {
            using (new AssertionScope())
            {
                if (dbAppearance is not null)
                {
                    context.Appearances.Any(dbAppearance1 => dbAppearance1.Id.Equals(dbAppearance.Id)).Should().BeTrue();
                }
                var addedDbAppearance = context.Appearances.First(dbAppearance1 => dbAppearance1.Id.Equals(addedAppearance.Id));
                addedDbAppearance.Should().NotBeNull();
                addedDbAppearance.Id.Should().NotBeEmpty().And.Be(addedAppearance.Id);
                addedDbAppearance.Title.Should().Be(expectedAppearance.Title);
                addedDbAppearance.Description.Should().Be(expectedAppearance.Description);
                addedDbAppearance.Color.Should().Be(expectedAppearance.Color);
                addedDbAppearance.TextColor.Should().Be(expectedAppearance.TextColor);
                addedDbAppearance.CreatedAt.Should().BeCloseTo(startTime, TimeSpan.FromMilliseconds(300), "Start time").And
                    .BeCloseTo(endTime, TimeSpan.FromMilliseconds(300), "End time");
                addedDbAppearance.LastModifiedAt.Should().BeNull();
            }
        });
    }

    private void AssertAppearanceNotAdded(GraphQLResponse<AddAppearanceResponse> response)
    {
        using (new AssertionScope())
        {
            response.Should().NotBeNull();
            response.Errors.Should().NotBeNullOrEmpty();
            response.Errors?.First().Message.Should().BeEmpty();
            response.Data.Should().BeNull();
        }

        CheckDbContent(context =>
        {
            context.Appearances.Should().BeEmpty();
        });
    }

    public static IEnumerable<Appearance> AddAppearanceCases()
    {
        var faker = new Faker<Appearance>()
            .RuleFor(appearance => appearance.Title, f => f.Random.String2(1, 50, AllowedChars))
            .RuleFor(appearance => appearance.Description, f => f.Random.String2(0, 255, AllowedChars).OrNull(f, 0.3f))
            .RuleFor(appearance => appearance.Color, f => f.Random.String2(6, 6, HexcodeColorChars))
            .RuleFor(appearance => appearance.TextColor, f => f.Random.String2(6, 6, HexcodeColorChars));
        return faker.Generate(20);
    }

    public static IEnumerable<Appearance> InvalidAddAppearanceCases()
    {
        yield return new Appearance { Title = null!, Description = null, Color = "001122", TextColor = "334455" };
        yield return new Appearance { Title = "", Description = null, Color = "001122", TextColor = "334455" };
        yield return new Appearance { Title = "  \t\n  ", Description = null, Color = "001122", TextColor = "334455" };
        yield return new Appearance { Title = "", Description = null, Color = "001122", TextColor = "334455" };
        yield return new Appearance { Title = new string('a', 51), Description = null, Color = "001122", TextColor = "334455" };
        yield return new Appearance { Title = "Valid", Description = new string('a', 256), Color = "001122", TextColor = "334455" };
        yield return new Appearance { Title = "Valid", Description = null, Color = null!, TextColor = "334455" };
        yield return new Appearance { Title = "Valid", Description = null, Color = "01122", TextColor = "334455" };
        yield return new Appearance { Title = "Valid", Description = null, Color = "", TextColor = "334455" };
        yield return new Appearance { Title = "Valid", Description = null, Color = " ", TextColor = "334455" };
        yield return new Appearance { Title = "Valid", Description = null, Color = "g", TextColor = "334455" };
        yield return new Appearance { Title = "Valid", Description = null, TextColor = null!, Color = "334455" };
        yield return new Appearance { Title = "Valid", Description = null, TextColor = "01122", Color = "334455" };
        yield return new Appearance { Title = "Valid", Description = null, TextColor = "", Color = "334455" };
        yield return new Appearance { Title = "Valid", Description = null, TextColor = " ", Color = "334455" };
        yield return new Appearance { Title = "Valid", Description = null, TextColor = "g", Color = "334455" };
    }
}
