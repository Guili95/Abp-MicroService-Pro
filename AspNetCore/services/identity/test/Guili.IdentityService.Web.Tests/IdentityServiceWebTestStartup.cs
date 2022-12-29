using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Guili.IdentityService;

public class IdentityServiceWebTestStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApplication<IdentityServiceWebTestModule>();
    }

    public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
    {
        app.InitializeApplication();
    }
}
