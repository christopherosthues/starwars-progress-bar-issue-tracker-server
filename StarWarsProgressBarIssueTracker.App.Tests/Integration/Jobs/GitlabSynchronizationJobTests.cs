using FluentAssertions;
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
        var deletedLabel = new DbLabel { Title = "Deleted", Color = "#fffff1", TextColor = "#fffff1", GitlabId = "gid://gitlab/ProjectLabel/4" };
        var githubLabel = new DbLabel { Title = "GitHub", Color = "#fffffe", TextColor = "#fffffe", GitHubId = "gid://github/ProjectLabel/5" };
        expectedDbLabels.Add(githubLabel);
        await SeedDatabaseAsync(context =>
        {
            context.Labels.Add(expectedDbLabels.First());
            context.Labels.Add(deletedLabel);
            context.Labels.Add(githubLabel);
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
                options => options.Excluding(dbLabel => dbLabel.Id).Excluding(dbLabel => dbLabel.CreatedAt));
            context.Labels.Should().ContainEquivalentOf(githubLabel,
                options => options.Excluding(dbLabel => dbLabel.Id).Excluding(dbLabel => dbLabel.CreatedAt));
            context.Labels.Should().ContainEquivalentOf(expectedDbLabels.First(),
                options => options.Excluding(dbLabel => dbLabel.Id).Excluding(dbLabel => dbLabel.CreatedAt));
        });

        // Act
        await job.ExecuteAsync(CancellationToken.None);

        // Assert
        CheckDbContent(context =>
        {
            context.Labels.Should().NotBeEmpty();
            var resultLabels = context.Labels.ToList();

            resultLabels.Should().NotContainEquivalentOf(deletedLabel,
                options => options.Excluding(dbLabel => dbLabel.Id).Excluding(dbLabel => dbLabel.CreatedAt));
            resultLabels.Should().BeEquivalentTo(expectedDbLabels,
                options => options.Excluding(dbLabel => dbLabel.Id).Excluding(dbLabel => dbLabel.CreatedAt));
        });
    }
}
