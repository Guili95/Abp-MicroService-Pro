using Guili.Shared.Hosting.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;

namespace Guili.Shared.Hosting.Gateways
{
    [DependsOn(
        typeof(GuiliSharedHostingAspNetCoreModule)
    )]
    public class GuiliSharedHostingGatewaysModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();

            Configure<AbpMultiTenancyOptions>(options =>
            {
                options.IsEnabled = Convert.ToBoolean(configuration["IsMultiTenancyEnabled"]);
            });

            context.Services.AddReverseProxy()
                .LoadFromConfig(configuration.GetSection("ReverseProxy"));
        }
    }
}
