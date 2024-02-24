using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using StarWarsProgressBarIssueTracker.App.Jobs;
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
    public async Task ExecuteAsyncShouldAddLabels()
    {
        // Arrange
        using var scope = ApiFactory.Services.CreateScope();
        var job = scope.ServiceProvider.GetRequiredService<GitlabSynchronizationJob>();
        _server.Given(Request.Create().WithPath("/api/graphql").UsingPost())
            .RespondWith(Response.Create().WithStatusCode(200)
                .WithHeader("Content-Type", "application/json")
                .WithBody(GitlabMockData.LabelResponse));

        // Act
        await job.ExecuteAsync(CancellationToken.None);

        // Assert
        var expectedDbLabels = GitlabMockData.AddedLabels();
        CheckDbContent(context =>
        {
            context.Labels.Should().NotBeEmpty();
            var resultLabels = context.Labels.ToList();

            resultLabels.Should().BeEquivalentTo(expectedDbLabels,
                options => options.Excluding(dbLabel => dbLabel.Id).Excluding(dbLabel => dbLabel.CreatedAt));
        });
    }
}
