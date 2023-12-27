using FluentAssertions;
using FluentAssertions.Execution;
using GraphQL;
using StarWarsProgressBarIssueTracker.App.Queries;
using StarWarsProgressBarIssueTracker.App.Tests.Helpers.GraphQL.Payloads.Releases;
using StarWarsProgressBarIssueTracker.Domain.Issues;
using StarWarsProgressBarIssueTracker.Domain.Milestones;
using StarWarsProgressBarIssueTracker.Domain.Releases;
using StarWarsProgressBarIssueTracker.Infrastructure.Models;

namespace StarWarsProgressBarIssueTracker.App.Tests.Integration.Queries;

[TestFixture(TestOf = typeof(IssueTrackerQueries))]
public class ReleaseQueriesTests : IntegrationTestBase
{
    [Test]
    public async Task GetReleasesShouldReturnEmptyListIfNoReleaseExist()
    {
        // Arrange
        CheckDbContent(context =>
        {
            context.Releases.Should().BeEmpty();
        });
        var request = CreateGetReleasesRequest();

        // Act
        var response = await GraphQLClient.SendQueryAsync<GetReleasesResponse>(request);

        // Assert
        using (new AssertionScope())
        {
            response.Should().NotBeNull();
            response.Errors.Should().BeNullOrEmpty();
            response.Data.Should().NotBeNull();
            response.Data.Releases.Should().BeEmpty();
        }
    }

    [Test]
    public async Task GetReleasesShouldReturnAllReleases()
    {
        // Arrange
        var dbRelease = new DbRelease
        {
            Title = "Release 1", ReleaseState = ReleaseState.Open
        };

        var dbIssue = new DbIssue
        {
            Title = "issue title",
            IssueState = IssueState.Closed,
            IssueType = IssueType.Vehicle,
            Milestone = new DbMilestone { Title = "milestone title", MilestoneState = MilestoneState.Open },
            Vehicle = new DbVehicle
            {
                Appearances =
                [
                    new DbAppearance { Title = "Appearance title", Color = "112233", TextColor = "334455" }
                ],
                Translations = [new DbTranslation { Country = "en", Text = "translation" }],
                Photos = [new DbPhoto { PhotoData = Array.Empty<byte>() }]
            }
        };
        var dbRelease2 = new DbRelease
        {
            Title = "Release 2", ReleaseNotes = "Notes 2", ReleaseState = ReleaseState.Closed, ReleaseDate = DateTime.UtcNow,
            LastModifiedAt = DateTime.UtcNow.AddDays(-1),
            Issues =
            [
                dbIssue
            ]
        };
        dbIssue.Release = dbRelease2;
        await SeedDatabase(context =>
        {
            context.Releases.Add(dbRelease);
            context.Releases.Add(dbRelease2);
        });
        CheckDbContent(context =>
        {
            var dbReleases = context.Releases.ToList();
            dbReleases.Any(release => release.Id.Equals(dbRelease.Id)).Should().BeTrue();
            dbReleases.Any(release => release.Id.Equals(dbRelease2.Id)).Should().BeTrue();
            var dbIssues = context.Issues.ToList();
            dbIssues.Any(issue => issue.Id.Equals(dbIssue.Id)).Should().BeTrue();
        });
        var request = CreateGetReleasesRequest();

        // Act
        var response = await GraphQLClient.SendQueryAsync<GetReleasesResponse>(request);

        // Assert
        using (new AssertionScope())
        {
            response.Should().NotBeNull();
            response.Errors.Should().BeNullOrEmpty();
            response.Data.Should().NotBeNull();
            var releases = response.Data.Releases.ToList();
            releases.Count.Should().Be(2);

            var release = releases.Single(entity => entity.Id.Equals(dbRelease.Id));
            release.Id.Should().Be(dbRelease.Id);
            release.Title.Should().Be(dbRelease.Title);
            release.ReleaseNotes.Should().BeNull();
            release.ReleaseState.Should().Be(dbRelease.ReleaseState);
            release.ReleaseDate.Should().BeNull();
            release.CreatedAt.Should().BeCloseTo(dbRelease.CreatedAt, TimeSpan.FromMilliseconds(300));
            release.LastModifiedAt.Should().Be(dbRelease.LastModifiedAt);
            release.Issues.Should().BeEmpty();

            var release2 = releases.Single(entity => entity.Id.Equals(dbRelease2.Id));
            release2.Id.Should().Be(dbRelease2.Id);
            release2.Title.Should().Be(dbRelease2.Title);
            release2.ReleaseNotes.Should().Be(dbRelease2.ReleaseNotes);
            release2.ReleaseState.Should().Be(dbRelease2.ReleaseState);
            release2.ReleaseDate.Should().BeCloseTo(dbRelease2.ReleaseDate!.Value, TimeSpan.FromMilliseconds(300));
            release2.CreatedAt.Should().BeCloseTo(dbRelease2.CreatedAt, TimeSpan.FromMilliseconds(300));
            release2.LastModifiedAt.Should().BeCloseTo(dbRelease2.LastModifiedAt!.Value, TimeSpan.FromMilliseconds(300));
            release2.Issues.Should().NotBeEmpty();
            release2.Issues.Count().Should().Be(1);
            var issue = release2.Issues.First();
            issue.Id.Should().Be(dbIssue.Id);
            issue.Milestone.Should().NotBeNull();
            issue.Milestone!.Id.Should().Be(dbIssue.Milestone.Id);
            issue.Vehicle.Should().NotBeNull();
            issue.Vehicle!.Id.Should().Be(dbIssue.Vehicle.Id);
            issue.Vehicle!.Translations.Should().BeEmpty();
            issue.Vehicle!.Photos.Should().BeEmpty();
        }
    }

    [Test]
    public async Task GetReleaseShouldReturnNullIfReleaseWithGivenIdDoesNotExist()
    {
        // Arrange
        await SeedDatabase(context =>
        {
            context.Releases.Add(new DbRelease
            {
                Id = new Guid("5888CDB6-57E2-4774-B6E8-7AABE82E2A5F"),
                Title = "Release 1", ReleaseNotes = "Notes 1", ReleaseState = ReleaseState.Closed, ReleaseDate = DateTime.UtcNow,
                LastModifiedAt = DateTime.UtcNow.AddDays(-1)
            });
        });
        const string id = "F1378377-9846-4168-A595-E763CD61CD9F";
        var request = CreateGetReleaseRequest(id);

        // Act
        var response = await GraphQLClient.SendQueryAsync<GetReleaseResponse>(request);

        // Assert
        using (new AssertionScope())
        {
            response.Should().NotBeNull();
            response.Errors.Should().BeNull();
            response.Data.Should().NotBeNull();
            response.Data.Release.Should().BeNull();
        }
    }

    [Test]
    public async Task GetReleaseShouldReturnNullIfReleasesAreEmpty()
    {
        // Arrange
        CheckDbContent(context =>
        {
            context.Releases.Should().BeEmpty();
        });
        const string id = "F1378377-9846-4168-A595-E763CD61CD9F";
        var request = CreateGetReleaseRequest(id);

        // Act
        var response = await GraphQLClient.SendQueryAsync<GetReleaseResponse>(request);

        // Assert
        using (new AssertionScope())
        {
            response.Should().NotBeNull();
            response.Errors.Should().BeNull();
            response.Data.Should().NotBeNull();
            response.Data.Release.Should().BeNull();
        }
    }

    [Test]
    public async Task GetReleaseShouldReturnReleaseWithGivenId()
    {
        // Arrange
        const string id = "F1378377-9846-4168-A595-E763CD61CD9F";
        var dbIssue = new DbIssue
        {
            Title = "issue title",
            IssueState = IssueState.Closed,
            IssueType = IssueType.Vehicle,
            Milestone = new DbMilestone { Title = "milestone title", MilestoneState = MilestoneState.Open },
            Vehicle = new DbVehicle
            {
                Appearances =
                [
                    new DbAppearance { Title = "Appearance title", Color = "112233", TextColor = "334455" }
                ],
                Translations = [new DbTranslation { Country = "en", Text = "translation" }],
                Photos = [new DbPhoto { PhotoData = Array.Empty<byte>() }]
            }
        };
        var dbRelease = new DbRelease
        {
            Id = new Guid(id),
            Title = "Release 2", ReleaseNotes = "Notes 2", ReleaseState = ReleaseState.Closed, ReleaseDate = DateTime.UtcNow,
            LastModifiedAt = DateTime.UtcNow.AddDays(-1),
            Issues = [dbIssue]
        };
        await SeedDatabase(context =>
        {
            context.Releases.Add(new DbRelease
            {
                Id = new Guid("5888CDB6-57E2-4774-B6E8-7AABE82E2A5F"),
                Title = "Release 1", ReleaseState = ReleaseState.Open
            });
            context.Releases.Add(dbRelease);
        });
        var request = CreateGetReleaseRequest(id);

        // Act
        var response = await GraphQLClient.SendQueryAsync<GetReleaseResponse>(request);

        // Assert
        using (new AssertionScope())
        {
            response.Should().NotBeNull();
            response.Errors.Should().BeNull();
            response.Data.Should().NotBeNull();
            var release = response.Data.Release;

            release.Should().NotBeNull();
            release!.Id.Should().Be(dbRelease.Id);
            release.Title.Should().Be(dbRelease.Title);
            release.ReleaseNotes.Should().Be(dbRelease.ReleaseNotes);
            release.ReleaseState.Should().Be(dbRelease.ReleaseState);
            release.ReleaseDate.Should().BeCloseTo(dbRelease.ReleaseDate!.Value, TimeSpan.FromMilliseconds(300));
            release.CreatedAt.Should().BeCloseTo(dbRelease.CreatedAt, TimeSpan.FromMilliseconds(300));
            release.LastModifiedAt.Should().BeCloseTo(dbRelease.LastModifiedAt!.Value, TimeSpan.FromMilliseconds(300));
            release.Issues.Should().NotBeEmpty();
            release.Issues.Count().Should().Be(1);
            var issue = release.Issues.First();
            issue.Id.Should().Be(dbIssue.Id);
            issue.Milestone.Should().NotBeNull();
            issue.Milestone!.Id.Should().Be(dbIssue.Milestone.Id);
            issue.Vehicle.Should().NotBeNull();
            issue.Vehicle!.Id.Should().Be(dbIssue.Vehicle.Id);
            issue.Vehicle!.Translations.Should().BeEmpty();
            issue.Vehicle!.Photos.Should().BeEmpty();
        }
    }

    private static GraphQLRequest CreateGetReleasesRequest()
    {
        var queryRequest = new GraphQLRequest
        {
            Query = """
                    query releases
                    {
                        releases()
                        {
                            id
                            title
                            releaseNotes
                            releaseState
                            releaseDate
                            createdAt
                            lastModifiedAt
                            issues
                            {
                                id
                                title
                                milestone
                                {
                                    id
                                    title
                                }
                                vehicle
                                {
                                    id
                                    appearances
                                    {
                                        id
                                        title
                                        color
                                        textColor
                                    }
                                }
                            }
                        }
                    }
                    """,
            OperationName = "releases"
        };
        return queryRequest;
    }

    private static GraphQLRequest CreateGetReleaseRequest(string id)
    {
        var queryRequest = new GraphQLRequest
        {
            Query = $$"""
                    query release
                    {
                        release(id: "{{id}}")
                        {
                            id
                            title
                            releaseNotes
                            releaseState
                            releaseDate
                            createdAt
                            lastModifiedAt
                            issues
                            {
                                id
                                title
                                milestone
                                {
                                    id
                                    title
                                }
                                vehicle
                                {
                                    id
                                    appearances
                                    {
                                        id
                                        title
                                        color
                                        textColor
                                    }
                                }
                            }
                        }
                    }
                    """,
            OperationName = "release"
        };
        return queryRequest;
    }
}
