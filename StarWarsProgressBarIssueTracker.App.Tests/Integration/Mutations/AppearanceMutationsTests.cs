using Bogus;
using FluentAssertions;
using FluentAssertions.Execution;
using GraphQL;
using Microsoft.EntityFrameworkCore;
using StarWarsProgressBarIssueTracker.App.Mutations;
using StarWarsProgressBarIssueTracker.App.Tests.Helpers.GraphQL.Payloads.Appearances;
using StarWarsProgressBarIssueTracker.Domain.Vehicles;
using StarWarsProgressBarIssueTracker.Infrastructure.Models;

namespace StarWarsProgressBarIssueTracker.App.Tests.Integration.Mutations;

[TestFixture(TestOf = typeof(IssueTrackerMutations))]
public class AppearanceMutationsTests : IntegrationTestBase
{
    private const string AllowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789ß_#%";
    private const string HexCodeColorChars = "0123456789abcdef";

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
            LastModifiedAt = DateTime.UtcNow.AddDays(1)
        };
        await SeedDatabase(context =>
        {
            context.Appearances.Add(dbAppearance);
        });
        CheckDbContent(context =>
        {
            context.Appearances.Should().Contain(dbAppearance);
        });
        var mutationRequest = CreateAddRequest(expectedAppearance);

        var startTime = DateTime.UtcNow;

        // Act
        var response = await GraphQLClient.SendMutationAsync<AddAppearanceResponse>(mutationRequest);

        // Assert
        AssertAddedAppearance(response, expectedAppearance, startTime, dbAppearance);
    }

    [TestCaseSource(nameof(InvalidAddAppearanceCases))]
    public async Task AddAppearanceShouldNotAddAppearance((Appearance expectedAppearance, IEnumerable<string> errors) expectedResult)
    {
        // Arrange
        CheckDbContent(context =>
        {
            context.Appearances.Should().BeEmpty();
        });
        var mutationRequest = CreateAddRequest(expectedResult.expectedAppearance);

        // Act
        var response = await GraphQLClient.SendMutationAsync<AddAppearanceResponse>(mutationRequest);

        // Assert
        AssertAppearanceNotAdded(response, expectedResult.errors);
    }

    [TestCaseSource(nameof(AddAppearanceCases))]
    public async Task UpdateAppearanceShouldUpdateAppearance(Appearance expectedAppearance)
    {
        // Arrange
        var dbAppearance = new DbAppearance
        {
            Id = new Guid("87653DC5-B029-4BA6-959A-1FBFC48E2C81"),
            Title = "Title",
            Description = "Desc",
            Color = "001122",
            TextColor = "334455",
            LastModifiedAt = DateTime.UtcNow.AddDays(1)
        };
        await SeedDatabase(context =>
        {
            context.Appearances.Add(dbAppearance);
        });
        expectedAppearance.Id = dbAppearance.Id;
        expectedAppearance.CreatedAt = dbAppearance.CreatedAt;
        CheckDbContent(context =>
        {
            context.Appearances.Should().Contain(dbAppearance);
        });
        var mutationRequest = CreateUpdateRequest(expectedAppearance);

        var startTime = DateTime.UtcNow;

        // Act
        var response = await GraphQLClient.SendMutationAsync<UpdateAppearanceResponse>(mutationRequest);

        // Assert
        AssertUpdatedAppearance(response, expectedAppearance, startTime);
    }

    [TestCaseSource(nameof(AddAppearanceCases))]
    public async Task UpdateAppearanceShouldUpdateAppearanceIfAppearancesAreNotEmpty(Appearance expectedAppearance)
    {
        // Arrange
        var dbAppearance = new DbAppearance
        {
            Id = new Guid("87653DC5-B029-4BA6-959A-1FBFC48E2C81"),
            Title = "Title",
            Description = "Desc",
            Color = "001122",
            TextColor = "334455",
            LastModifiedAt = DateTime.UtcNow.AddDays(1)
        };
        var dbAppearance2 = new DbAppearance
        {
            Id = new Guid("0609F93C-CBCC-4650-BA4C-B8D5FF93A877"),
            Title = "Title 2",
            Description = "Desc 2",
            Color = "221100",
            TextColor = "554433",
            LastModifiedAt = DateTime.UtcNow.AddDays(2)
        };


        await SeedDatabase(context =>
        {
            context.Appearances.Add(dbAppearance);
            context.Appearances.Add(dbAppearance2);
        });
        expectedAppearance.Id = dbAppearance.Id;
        expectedAppearance.CreatedAt = dbAppearance.CreatedAt;
        CheckDbContent(context =>
        {
            context.Appearances.Should().Contain(dbAppearance);
            context.Appearances.Should().Contain(dbAppearance2);
        });
        var mutationRequest = CreateUpdateRequest(expectedAppearance);

        var startTime = DateTime.UtcNow;

        // Act
        var response = await GraphQLClient.SendMutationAsync<UpdateAppearanceResponse>(mutationRequest);

        // Assert
        AssertUpdatedAppearance(response, expectedAppearance, startTime, dbAppearance, dbAppearance2);
    }

    [TestCaseSource(nameof(InvalidAddAppearanceCases))]
    public async Task UpdateAppearanceShouldNotUpdateAppearance((Appearance expectedAppearance, IEnumerable<string> errors) expectedResult)
    {
        // Arrange
        CheckDbContent(context =>
        {
            context.Appearances.Should().BeEmpty();
        });
        var mutationRequest = CreateUpdateRequest(expectedResult.expectedAppearance);

        // Act
        var response = await GraphQLClient.SendMutationAsync<UpdateAppearanceResponse>(mutationRequest);

        // Assert
        AssertAppearanceNotUpdated(response, expectedResult.errors);
    }

    [Test]
    public async Task UpdateAppearanceShouldNotUpdateAppearanceIfAppearanceDoesNotExist()
    {
        // Arrange
        var appearance = CreateAppearance();
        CheckDbContent(context =>
        {
            context.Appearances.Should().BeEmpty();
        });
        var mutationRequest = CreateUpdateRequest(appearance);

        // Act
        var response = await GraphQLClient.SendMutationAsync<UpdateAppearanceResponse>(mutationRequest);

        // Assert
        AssertAppearanceNotUpdated(response, new List<string> { $"No {nameof(Appearance)} found with id '{appearance.Id}'." });
    }

    [Test]
    public async Task DeleteAppearanceShouldDeleteAppearance()
    {
        // Arrange
        var appearance = CreateAppearance();
        var dbAppearance = new DbAppearance
        {
            Id = appearance.Id,
            Title = appearance.Title,
            Description = appearance.Description,
            Color = appearance.Color,
            TextColor = appearance.TextColor,
            LastModifiedAt = DateTime.UtcNow.AddDays(1)
        };
        await SeedDatabase(context =>
        {
            context.Appearances.Add(dbAppearance);
        });
        appearance.CreatedAt = dbAppearance.CreatedAt;
        appearance.LastModifiedAt = dbAppearance.LastModifiedAt;
        CheckDbContent(context =>
        {
            context.Appearances.Should().Contain(dbAppearance);
        });
        var mutationRequest = CreateDeleteRequest(appearance);

        // Act
        var response = await GraphQLClient.SendMutationAsync<DeleteAppearanceResponse>(mutationRequest);

        // Assert
        AssertDeletedAppearance(response, appearance);
    }

    [Test]
    public async Task DeleteAppearanceShouldDeleteAppearanceAndReferenceToVehicles()
    {
        // Arrange
        var appearance = CreateAppearance();
        var dbAppearance = new DbAppearance
        {
            Id = appearance.Id,
            Title = appearance.Title,
            Description = appearance.Description,
            Color = appearance.Color,
            TextColor = appearance.TextColor,
            LastModifiedAt = DateTime.UtcNow.AddDays(1)
        };
        var dbAppearance2 = new DbAppearance
        {
            Id = new Guid("B961A621-9848-429A-8B44-B1AF1F0182CE"),
            Color = "778899",
            TextColor = "665544",
            Title = "Title 2"
        };
        var dbVehicle2 = new DbVehicle
        {
            Id = new Guid("74AE8DD4-7669-4428-8E81-FB8A24A217A3"),
            EngineColor = EngineColor.Green,
            Appearances =
            [
                dbAppearance,
                dbAppearance2
            ]
        };
        await SeedDatabase(context =>
        {
            var dbVehicle = new DbVehicle
            {
                Id = new Guid("87A2F9BF-CAB7-41D3-84F9-155135FA41D7"), EngineColor = EngineColor.Blue, Appearances = [dbAppearance]
            };
            context.Appearances.Add(dbAppearance);
            context.Vehicles.Add(dbVehicle);
            context.Vehicles.Add(dbVehicle2);
        });
        appearance.CreatedAt = dbAppearance.CreatedAt;
        appearance.LastModifiedAt = dbAppearance.LastModifiedAt;
        CheckDbContent(context =>
        {
            context.Appearances.Should().Contain(dbAppearance);
        });
        var mutationRequest = CreateDeleteRequest(appearance);

        // Act
        var response = await GraphQLClient.SendMutationAsync<DeleteAppearanceResponse>(mutationRequest);

        // Assert
        AssertDeletedAppearance(response, appearance);
        CheckDbContent(context =>
        {
            var dbVehicles = context.Vehicles.Include(dbVehicle => dbVehicle.Appearances).ToList();
            foreach (var dbVehicle in dbVehicles)
            {
                dbVehicle.Appearances.Should().NotContain(dbAppearance);
            }

            dbVehicles.First(dbVehicle => dbVehicle.Id.Equals(dbVehicle2.Id)).Appearances.Should().Contain(dbAppearance2);
        });
    }

    [Test]
    public async Task DeleteAppearanceShouldDeleteAppearanceIfAppearancesIsNotEmpty()
    {
        // Arrange
        var appearance = CreateAppearance();
        var dbAppearance = new DbAppearance
        {
            Id = appearance.Id,
            Title = appearance.Title,
            Description = appearance.Description,
            Color = appearance.Color,
            TextColor = appearance.TextColor,
            LastModifiedAt = DateTime.UtcNow.AddDays(1)
        };
        var dbAppearance2 = new DbAppearance
        {
            Id = new Guid("0609F93C-CBCC-4650-BA4C-B8D5FF93A877"),
            Title = "Title 2",
            Description = "Desc 2",
            Color = "221100",
            TextColor = "554433",
            CreatedAt = DateTime.UtcNow.AddDays(-3),
            LastModifiedAt = DateTime.UtcNow.AddDays(-2)
        };


        await SeedDatabase(context =>
        {
            context.Appearances.Add(dbAppearance);
            context.Appearances.Add(dbAppearance2);
        });
        appearance.CreatedAt = dbAppearance.CreatedAt;
        appearance.LastModifiedAt = dbAppearance.LastModifiedAt;
        CheckDbContent(context =>
        {
            context.Appearances.Should().Contain(dbAppearance);
            context.Appearances.Should().Contain(dbAppearance2);
        });
        var mutationRequest = CreateDeleteRequest(appearance);

        // Act
        var response = await GraphQLClient.SendMutationAsync<DeleteAppearanceResponse>(mutationRequest);

        // Assert
        AssertDeletedAppearance(response, appearance, dbAppearance2);
    }

    [Test]
    public async Task DeleteAppearanceShouldNotDeleteAppearanceIfAppearanceDoesNotExist()
    {
        // Arrange
        var appearance = CreateAppearance();
        CheckDbContent(context =>
        {
            context.Appearances.Should().BeEmpty();
        });
        var mutationRequest = CreateDeleteRequest(appearance);

        // Act
        var response = await GraphQLClient.SendMutationAsync<DeleteAppearanceResponse>(mutationRequest);

        // Assert
        AssertAppearanceNotDeleted(response, new List<string> { $"No {nameof(Appearance)} found with id '{appearance.Id}'." });
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
                              },
                              errors
                              {
                                  ... on Error
                                  {
                                      message
                                  }
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
            addedAppearance.CreatedAt.Should().BeCloseTo(startTime, TimeSpan.FromSeconds(1), "Start time").And
                .BeCloseTo(endTime, TimeSpan.FromSeconds(1), "End time");
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
                addedDbAppearance.CreatedAt.Should().BeCloseTo(startTime, TimeSpan.FromSeconds(1), "Start time").And
                    .BeCloseTo(endTime, TimeSpan.FromSeconds(1), "End time");
                addedDbAppearance.LastModifiedAt.Should().BeNull();
            }
        });
    }

    private void AssertAppearanceNotAdded(GraphQLResponse<AddAppearanceResponse> response, IEnumerable<string> errors)
    {
        using (new AssertionScope())
        {
            response.Should().NotBeNull();
            response.Data.AddAppearance.Errors.Should().NotBeNullOrEmpty();
            response.Data.AddAppearance.Appearance.Should().BeNull();

            var resultErrors = response.Data.AddAppearance.Errors.Select(error => error.Message);
            resultErrors.Should().BeEquivalentTo(errors);
        }

        CheckDbContent(context =>
        {
            context.Appearances.Should().BeEmpty();
        });
    }

    private static GraphQLRequest CreateUpdateRequest(Appearance expectedAppearance)
    {
        var descriptionParameter = expectedAppearance.Description != null
            ? $"""
               , description: "{expectedAppearance.Description}"
               """
            : string.Empty;
        var mutationRequest = new GraphQLRequest
        {
            Query = $$"""
                      mutation updateAppearance
                      {
                          updateAppearance(input: {id: "{{expectedAppearance.Id}}", title: "{{expectedAppearance.Title}}", color: "{{expectedAppearance.Color}}", textColor: "{{expectedAppearance.TextColor}}"{{descriptionParameter}}})
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
                              },
                              errors
                              {
                                  ... on Error
                                  {
                                      message
                                  }
                              }
                          }
                      }
                      """,
            OperationName = "updateAppearance"
        };
        return mutationRequest;
    }

    private void AssertUpdatedAppearance(GraphQLResponse<UpdateAppearanceResponse> response, Appearance expectedAppearance,
        DateTime startTime, DbAppearance? dbAppearance = null, DbAppearance? notUpdatedDbAppearance = null)
    {
        DateTime endTime = DateTime.UtcNow;
        Appearance? updatedAppearance;
        using (new AssertionScope())
        {
            response.Should().NotBeNull();
            response.Errors.Should().BeNullOrEmpty();
            updatedAppearance = response.Data.UpdateAppearance.Appearance;
            updatedAppearance.Id.Should().Be(expectedAppearance.Id);
            updatedAppearance.Title.Should().Be(expectedAppearance.Title);
            updatedAppearance.Description.Should().Be(expectedAppearance.Description);
            updatedAppearance.Color.Should().Be(expectedAppearance.Color);
            updatedAppearance.TextColor.Should().Be(expectedAppearance.TextColor);
            updatedAppearance.CreatedAt.Should().BeCloseTo(expectedAppearance.CreatedAt, TimeSpan.FromSeconds(1));
            updatedAppearance.LastModifiedAt.Should().BeCloseTo(startTime, TimeSpan.FromSeconds(1), "Start time").And
                .BeCloseTo(endTime, TimeSpan.FromSeconds(1), "End time");
        }

        CheckDbContent(context =>
        {
            using (new AssertionScope())
            {
                if (dbAppearance is not null)
                {
                    context.Appearances.Any(dbAppearance1 => dbAppearance1.Id.Equals(dbAppearance.Id)).Should().BeTrue();
                }
                var updatedDbAppearance = context.Appearances.First(dbAppearance1 => dbAppearance1.Id.Equals(updatedAppearance.Id));
                updatedDbAppearance.Should().NotBeNull();
                updatedDbAppearance.Id.Should().NotBeEmpty().And.Be(updatedAppearance.Id);
                updatedDbAppearance.Title.Should().Be(expectedAppearance.Title);
                updatedDbAppearance.Description.Should().Be(expectedAppearance.Description);
                updatedDbAppearance.Color.Should().Be(expectedAppearance.Color);
                updatedDbAppearance.TextColor.Should().Be(expectedAppearance.TextColor);
                updatedDbAppearance.CreatedAt.Should().BeCloseTo(expectedAppearance.CreatedAt, TimeSpan.FromSeconds(1));
                updatedDbAppearance.LastModifiedAt.Should().BeCloseTo(startTime, TimeSpan.FromSeconds(1), "Start time").And
                    .BeCloseTo(endTime, TimeSpan.FromSeconds(1), "End time");

                if (notUpdatedDbAppearance is not null)
                {
                    var secondDbAppearance =
                        context.Appearances.FirstOrDefault(appearance => appearance.Id.Equals(notUpdatedDbAppearance.Id));
                    secondDbAppearance.Should().NotBeNull();
                    secondDbAppearance!.Id.Should().NotBeEmpty().And.Be(notUpdatedDbAppearance.Id);
                    secondDbAppearance.Title.Should().Be(notUpdatedDbAppearance.Title);
                    secondDbAppearance.Description.Should().Be(notUpdatedDbAppearance.Description);
                    secondDbAppearance.Color.Should().Be(notUpdatedDbAppearance.Color);
                    secondDbAppearance.TextColor.Should().Be(notUpdatedDbAppearance.TextColor);
                    secondDbAppearance.CreatedAt.Should().BeCloseTo(notUpdatedDbAppearance.CreatedAt, TimeSpan.FromSeconds(1));
                    secondDbAppearance.LastModifiedAt.Should().BeCloseTo(notUpdatedDbAppearance.LastModifiedAt!.Value, TimeSpan.FromSeconds(1));
                }
            }
        });
    }

    private void AssertAppearanceNotUpdated(GraphQLResponse<UpdateAppearanceResponse> response, IEnumerable<string> errors)
    {
        using (new AssertionScope())
        {
            response.Should().NotBeNull();
            response.Data.UpdateAppearance.Errors.Should().NotBeNullOrEmpty();
            response.Data.UpdateAppearance.Appearance.Should().BeNull();

            var resultErrors = response.Data.UpdateAppearance.Errors.Select(error => error.Message);
            resultErrors.Should().BeEquivalentTo(errors);
        }

        CheckDbContent(context =>
        {
            context.Appearances.Should().BeEmpty();
        });
    }

    private static GraphQLRequest CreateDeleteRequest(Appearance expectedAppearance)
    {
        var mutationRequest = new GraphQLRequest
        {
            Query = $$"""
                      mutation deleteAppearance
                      {
                          deleteAppearance(input: {id: "{{expectedAppearance.Id}}"})
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
                              },
                              errors
                              {
                                  ... on Error
                                  {
                                      message
                                  }
                              }
                          }
                      }
                      """,
            OperationName = "deleteAppearance"
        };
        return mutationRequest;
    }

    private void AssertDeletedAppearance(GraphQLResponse<DeleteAppearanceResponse> response, Appearance expectedAppearance, DbAppearance? dbAppearance = null)
    {
        Appearance? deletedAppearance;
        using (new AssertionScope())
        {
            response.Should().NotBeNull();
            response.Errors.Should().BeNullOrEmpty();
            deletedAppearance = response.Data.DeleteAppearance.Appearance;
            deletedAppearance.Id.Should().NotBeEmpty();
            deletedAppearance.Title.Should().Be(expectedAppearance.Title);
            deletedAppearance.Description.Should().Be(expectedAppearance.Description);
            deletedAppearance.Color.Should().Be(expectedAppearance.Color);
            deletedAppearance.TextColor.Should().Be(expectedAppearance.TextColor);
            deletedAppearance.CreatedAt.Should().BeCloseTo(expectedAppearance.CreatedAt, TimeSpan.FromSeconds(1));
            deletedAppearance.LastModifiedAt.Should().BeCloseTo(expectedAppearance.LastModifiedAt!.Value, TimeSpan.FromSeconds(1));
        }

        CheckDbContent(context =>
        {
            using (new AssertionScope())
            {
                context.Appearances.Any(dbAppearance1 => dbAppearance1.Id.Equals(expectedAppearance.Id)).Should().BeFalse();

                if (dbAppearance is not null)
                {
                    context.Appearances.Any(dbAppearance1 => dbAppearance1.Id.Equals(dbAppearance.Id)).Should().BeTrue();
                }
            }
        });
    }

    private void AssertAppearanceNotDeleted(GraphQLResponse<DeleteAppearanceResponse> response, IEnumerable<string> errors)
    {
        using (new AssertionScope())
        {
            response.Should().NotBeNull();
            response.Data.DeleteAppearance.Errors.Should().NotBeNullOrEmpty();
            response.Data.DeleteAppearance.Appearance.Should().BeNull();

            var resultErrors = response.Data.DeleteAppearance.Errors.Select(error => error.Message);
            resultErrors.Should().BeEquivalentTo(errors);
        }

        CheckDbContent(context =>
        {
            context.Appearances.Should().BeEmpty();
        });
    }

    private static Appearance CreateAppearance()
    {
        var faker = new Faker<Appearance>()
            .RuleFor(appearance => appearance.Id, f => f.Random.Guid())
            .RuleFor(appearance => appearance.Title, f => f.Random.String2(1, 50, AllowedChars))
            .RuleFor(appearance => appearance.Description, f => f.Random.String2(0, 255, AllowedChars).OrNull(f, 0.3f))
            .RuleFor(appearance => appearance.Color, f => f.Random.String2(6, 6, HexCodeColorChars))
            .RuleFor(appearance => appearance.TextColor, f => f.Random.String2(6, 6, HexCodeColorChars));
        return faker.Generate();
    }

    public static IEnumerable<Appearance> AddAppearanceCases()
    {
        var faker = new Faker<Appearance>()
            .RuleFor(appearance => appearance.Title, f => f.Random.String2(1, 50, AllowedChars))
            .RuleFor(appearance => appearance.Description, f => f.Random.String2(0, 255, AllowedChars).OrNull(f, 0.3f))
            .RuleFor(appearance => appearance.Color, f => f.Random.String2(6, 6, HexCodeColorChars))
            .RuleFor(appearance => appearance.TextColor, f => f.Random.String2(6, 6, HexCodeColorChars));
        return faker.Generate(20);
    }

    public static IEnumerable<(Appearance, IEnumerable<string>)> InvalidAddAppearanceCases()
    {
        yield return (new Appearance { Title = null!, Description = null, Color = "001122", TextColor = "334455" }, new List<string> { $"The value for {nameof(Appearance.Title)} is not set.", $"The value '' for {nameof(Appearance.Title)} is too short. The length of {nameof(Appearance.Title)} has to be between 1 and 50." });
        yield return (new Appearance { Title = "", Description = null, Color = "001122", TextColor = "334455" }, new List<string> { $"The value for {nameof(Appearance.Title)} is not set.", $"The value '' for {nameof(Appearance.Title)} is too short. The length of {nameof(Appearance.Title)} has to be between 1 and 50." });
        yield return (new Appearance { Title = "  \t ", Description = null, Color = "001122", TextColor = "334455" }, new List<string> { $"The value for {nameof(Appearance.Title)} is not set." });
        yield return (new Appearance { Title = new string('a', 51), Description = null, Color = "001122", TextColor = "334455" }, new List<string> { $"The value 'aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa' for {nameof(Appearance.Title)} is long short. The length of {nameof(Appearance.Title)} has to be between 1 and 50." });
        yield return (new Appearance { Title = "Valid", Description = new string('a', 256), Color = "001122", TextColor = "334455" }, new List<string> { $"The value 'aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa' for {nameof(Appearance.Description)} is long short. The length of {nameof(Appearance.Description)} has to be less than 256." });
        yield return (new Appearance { Title = "Valid", Description = null, Color = null!, TextColor = "334455" }, new List<string> { $"The value for {nameof(Appearance.Color)} is not set.", $"The value '' for field {nameof(Appearance.Color)} has a wrong format. Only colors in RGB hex format with 6 digits are allowed." });
        yield return (new Appearance { Title = "Valid", Description = null, Color = "01122", TextColor = "334455" }, new List<string> { $"The value '01122' for field {nameof(Appearance.Color)} has a wrong format. Only colors in RGB hex format with 6 digits are allowed." });
        yield return (new Appearance { Title = "Valid", Description = null, Color = "", TextColor = "334455" }, new List<string> { $"The value for {nameof(Appearance.Color)} is not set.", $"The value '' for field {nameof(Appearance.Color)} has a wrong format. Only colors in RGB hex format with 6 digits are allowed." });
        yield return (new Appearance { Title = "Valid", Description = null, Color = " ", TextColor = "334455" }, new List<string> { $"The value for {nameof(Appearance.Color)} is not set.", $"The value ' ' for field {nameof(Appearance.Color)} has a wrong format. Only colors in RGB hex format with 6 digits are allowed." });
        yield return (new Appearance { Title = "Valid", Description = null, Color = "g", TextColor = "334455" }, new List<string> { $"The value 'g' for field {nameof(Appearance.Color)} has a wrong format. Only colors in RGB hex format with 6 digits are allowed." });
        yield return (new Appearance { Title = "Valid", Description = null, TextColor = null!, Color = "334455" }, new List<string> { $"The value for {nameof(Appearance.TextColor)} is not set.", $"The value '' for field {nameof(Appearance.TextColor)} has a wrong format. Only colors in RGB hex format with 6 digits are allowed." });
        yield return (new Appearance { Title = "Valid", Description = null, TextColor = "01122", Color = "334455" }, new List<string> { $"The value '01122' for field {nameof(Appearance.TextColor)} has a wrong format. Only colors in RGB hex format with 6 digits are allowed." });
        yield return (new Appearance { Title = "Valid", Description = null, TextColor = "", Color = "334455" }, new List<string> { $"The value for {nameof(Appearance.TextColor)} is not set.", $"The value '' for field {nameof(Appearance.TextColor)} has a wrong format. Only colors in RGB hex format with 6 digits are allowed." });
        yield return (new Appearance { Title = "Valid", Description = null, TextColor = " ", Color = "334455" }, new List<string> { $"The value for {nameof(Appearance.TextColor)} is not set.", $"The value ' ' for field {nameof(Appearance.TextColor)} has a wrong format. Only colors in RGB hex format with 6 digits are allowed." });
        yield return (new Appearance { Title = "Valid", Description = null, TextColor = "g", Color = "334455" }, new List<string> { $"The value 'g' for field {nameof(Appearance.TextColor)} has a wrong format. Only colors in RGB hex format with 6 digits are allowed." });
    }
}
