using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Guili.AdministrationService;

public class AdministrationServiceWebTestStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApplication<AdministrationServiceWebTestModule>();
    }

    public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
    {
        app.InitializeApplication();
    }
}
