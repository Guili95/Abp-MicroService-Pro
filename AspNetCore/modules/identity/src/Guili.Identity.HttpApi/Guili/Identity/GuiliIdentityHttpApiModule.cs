using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;

namespace Guili.Identity
{
    [DependsOn(
        typeof(GuiliIdentityApplicationContractsModule),
        typeof(AbpIdentityHttpApiModule),
        typeof(AbpAspNetCoreMvcModule)
    )]
    public class GuiliIdentityHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(GuiliIdentityHttpApiModule).Assembly);
            });
        }
    }
}
