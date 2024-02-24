using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StarWarsProgressBarIssueTracker.App.Jobs;
using StarWarsProgressBarIssueTracker.Infrastructure.Models;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using WireMock.Settings;

namespace StarWarsProgressBarIssueTracker.App.Tests.Integration.Jobs;

[TestFixture(TestOf = typeof(GitlabSynchronizationJob))]
public class GitlabSynchronizationJobTests : IntegrationTestBase
{
    private WireMockServer _server = default!;

    [SetUp]
    public void SetUp()
    {
        _server = WireMockServer.Start(new WireMockServerSettings()
        {
            Port = 8081,
            UseSSL = true,
            CertificateSettings = new WireMockCertificateSettings()
            {
                X509StoreName = "My",
                X509StoreLocation = "CurrentUser",
            }
        });
    }

    [TearDown]
    public void TearDown()
    {
        _server.Stop();
        _server.Dispose();
    }

    [Test]
    public async Task ExecuteAsyncShouldUpdateLabels()
    {
        // Arrange
        var expectedDbLabels = GitlabMockData.AddedLabels();
        var dbIssue = new DbIssue() { Title = "NotDeleted", };
        var deletedLabel = new DbLabel
        {
            Title = "Deleted", Color = "#fffff1", TextColor = "#fffff1", GitlabId = "gid://gitlab/ProjectLabel/4",
            Issues = [dbIssue]
        };
        dbIssue.Labels.Add(deletedLabel);
        var githubLabel = new DbLabel { Title = "GitHub", Color = "#fffffe", TextColor = "#fffffe", GitHubId = "gid://github/ProjectLabel/5" };
        expectedDbLabels.Add(githubLabel);
        await SeedDatabaseAsync(context =>
        {
            context.Labels.Add(expectedDbLabels.First());
            context.Labels.Add(deletedLabel);
            context.Labels.Add(githubLabel);
            context.Issues.Add(dbIssue);
        });
        using var scope = ApiFactory.Services.CreateScope();
        var job = scope.ServiceProvider.GetRequiredService<GitlabSynchronizationJob>();
        _server.Given(Request.Create().WithPath("/api/graphql").UsingPost())
            .RespondWith(Response.Create().WithStatusCode(200)
                .WithHeader("Content-Type", "application/json")
                .WithBody(GitlabMockData.LabelResponse));

        CheckDbContent(context =>
        {
            context.Labels.Should().ContainEquivalentOf(deletedLabel,
                options => options.Excluding(dbLabel => dbLabel.Id).Excluding(dbLabel => dbLabel.CreatedAt)
                    .Excluding(dbLabel => dbLabel.Issues));
            context.Labels.Should().ContainEquivalentOf(githubLabel,
                options => options.Excluding(dbLabel => dbLabel.Id).Excluding(dbLabel => dbLabel.CreatedAt));
            context.Labels.Should().ContainEquivalentOf(expectedDbLabels.First(),
                options => options.Excluding(dbLabel => dbLabel.Id).Excluding(dbLabel => dbLabel.CreatedAt));
            context.Issues.Should().ContainEquivalentOf(dbIssue,
                options => options.Excluding(issue => issue.Id).Excluding(issue => issue.CreatedAt)
                    .Excluding(issue => issue.Labels));
        });

        // Act
        await job.ExecuteAsync(CancellationToken.None);

        // Assert
        CheckDbContent(context =>
        {
            context.Labels.Should().NotBeEmpty();
            var resultLabels = context.Labels.ToList();

            resultLabels.Should().HaveCount(expectedDbLabels.Count);
            resultLabels.Should().NotContainEquivalentOf(deletedLabel,
                options => options.Excluding(dbLabel => dbLabel.Id).Excluding(dbLabel => dbLabel.CreatedAt)
                    .Excluding(dbLabel => dbLabel.Issues));
            resultLabels.Should().BeEquivalentTo(expectedDbLabels,
                options => options.Excluding(dbLabel => dbLabel.Id).Excluding(dbLabel => dbLabel.CreatedAt));

            var issues = context.Issues.Include(issue => issue.Labels).ToList();
            issues.Should().ContainEquivalentOf(dbIssue, options => options.Excluding(issue => issue.Id)
                .Excluding(issue => issue.CreatedAt).Excluding(issue => issue.Labels));
            issues.First().Labels.Should().BeEmpty();
        });
    }

    [Test]
    public async Task ExecuteAsyncShouldUpdateMilestones()
    {
        // Arrange
        var expectedDbMilestones = GitlabMockData.AddedMilestones();
        var dbIssue = new DbIssue { Title = "NotDeleted", };
        var deletedMilestone = new DbMilestone
        {
            Title = "Deleted", GitlabId = "gid://gitlab/Milestone/4", GitlabIid = "4",
            Issues = [dbIssue]
        };
        dbIssue.Milestone = deletedMilestone;
        var githubMilestone = new DbMilestone { Title = "GitHub", GitHubId = "gid://github/Milestone/5", };
        expectedDbMilestones.Add(githubMilestone);
        await SeedDatabaseAsync(context =>
        {
            context.Milestones.Add(expectedDbMilestones.First());
            context.Milestones.Add(deletedMilestone);
            context.Milestones.Add(githubMilestone);
            context.Issues.Add(dbIssue);
        });
        using var scope = ApiFactory.Services.CreateScope();
        var job = scope.ServiceProvider.GetRequiredService<GitlabSynchronizationJob>();
        _server.Given(Request.Create().WithPath("/api/graphql").UsingPost())
            .RespondWith(Response.Create().WithStatusCode(200)
                .WithHeader("Content-Type", "application/json")
                .WithBody(GitlabMockData.MilestoneResponse));

        CheckDbContent(context =>
        {
            context.Milestones.Should().ContainEquivalentOf(deletedMilestone,
                options => options.Excluding(dbMilestone => dbMilestone.Id).Excluding(dbMilestone => dbMilestone.CreatedAt)
                    .Excluding(dbMilestone => dbMilestone.Issues));
            context.Milestones.Should().ContainEquivalentOf(githubMilestone,
                options => options.Excluding(dbMilestone => dbMilestone.Id).Excluding(dbMilestone => dbMilestone.CreatedAt));
            context.Milestones.Should().ContainEquivalentOf(expectedDbMilestones.First(),
                options => options.Excluding(dbMilestone => dbMilestone.Id).Excluding(dbMilestone => dbMilestone.CreatedAt));
            context.Issues.Should().ContainEquivalentOf(dbIssue,
                options => options.Excluding(issue => issue.Id).Excluding(issue => issue.CreatedAt)
                    .Excluding(issue => issue.Milestone));
        });

        // Act
        await job.ExecuteAsync(CancellationToken.None);

        // Assert
        CheckDbContent(context =>
        {
            context.Milestones.Should().NotBeEmpty();
            var resultMilestones = context.Milestones.ToList();

            resultMilestones.Should().HaveCount(expectedDbMilestones.Count);
            resultMilestones.Should().NotContainEquivalentOf(deletedMilestone,
                options => options.Excluding(dbMilestone => dbMilestone.Id).Excluding(dbMilestone => dbMilestone.CreatedAt)
                    .Excluding(dbMilestone => dbMilestone.Issues));
            resultMilestones.Should().BeEquivalentTo(expectedDbMilestones,
                options => options.Excluding(dbMilestone => dbMilestone.Id).Excluding(dbMilestone => dbMilestone.CreatedAt));

            var issues = context.Issues.Include(issue => issue.Milestone).ToList();
            issues.Should().ContainEquivalentOf(dbIssue, options => options.Excluding(issue => issue.Id)
                .Excluding(issue => issue.LastModifiedAt).Excluding(issue => issue.CreatedAt)
                .Excluding(issue => issue.Milestone));
            issues.First().Milestone.Should().BeNull();
        });
    }
}
