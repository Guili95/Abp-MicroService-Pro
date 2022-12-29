using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Volo.Abp;

namespace Guili.SaasService;

public class SaasServiceWebTestStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApplication<SaasServiceWebTestModule>();
    }

    public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
    {
        app.InitializeApplication();
    }
}
