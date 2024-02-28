using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using StarWarsProgressBarIssueTracker.Infrastructure.GitHub.Configuration;
using StarWarsProgressBarIssueTracker.Infrastructure.Gitlab.Configuration;

namespace StarWarsProgressBarIssueTracker.App.Tests.Integration.Setup;

public class IssueTrackerWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            Quartz.Logging.LogContext.SetCurrentLogProvider(NullLoggerFactory.Instance);

            services.Configure<GitlabConfiguration>(opts =>
            {
                opts.GraphQLUrl = "http://localhost:8081/api/graphql";
            });

            services.Configure<GitHubConfiguration>(opts =>
            {
                opts.GraphQLUrl = "http://localhost:8082";
            });

            services.ReplaceDbContext();

            services.AddGitlabClient().ConfigureHttpClient(client =>
                {
                    var gitlabGraphQlUrl = new Uri("http://localhost:8081/api/graphql");
                    client.BaseAddress = new UriBuilder(Uri.UriSchemeHttps, gitlabGraphQlUrl.Host, gitlabGraphQlUrl.Port, gitlabGraphQlUrl.PathAndQuery).Uri;
                    ServicePointManager.ServerCertificateValidationCallback += (_, _, _, _) => true;
                }
            ).ConfigureWebSocketClient(client =>
            {
                var gitlabGraphQlUrl = new Uri("http://localhost:8081/api/graphql");
                client.Uri = new UriBuilder(Uri.UriSchemeWs, gitlabGraphQlUrl.Host, gitlabGraphQlUrl.Port, gitlabGraphQlUrl.PathAndQuery).Uri;
                ServicePointManager.ServerCertificateValidationCallback += (_, _, _, _) => true;
            });
        });
    }
}
