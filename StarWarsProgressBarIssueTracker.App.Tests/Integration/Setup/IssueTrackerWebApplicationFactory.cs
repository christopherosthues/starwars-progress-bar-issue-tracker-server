using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Logging.Abstractions;

namespace StarWarsProgressBarIssueTracker.App.Tests.Integration.Setup;

public class IssueTrackerWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            Quartz.Logging.LogContext.SetCurrentLogProvider(NullLoggerFactory.Instance);

            services.ReplaceDbContext();
        });
    }
}
