using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace StarWarsProgressBarIssueTracker.App.Tests.Integration.Setup;

public class IssueTrackerWebApplicationFactory : WebApplicationFactory<Program>
{
    private IServiceProvider _serviceProvider = default!;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.ReplaceDbContext();

            _serviceProvider = services.BuildServiceProvider();
        });
    }

    public IServiceProvider GetServiceProvider()
    {
        return _serviceProvider;
    }
}
