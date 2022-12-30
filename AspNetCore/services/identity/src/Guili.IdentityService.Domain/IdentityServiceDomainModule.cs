using Guili.Identity;
using Volo.Abp.IdentityServer;
using Volo.Abp.Modularity;

namespace Guili.IdentityService
{
    [DependsOn(
        typeof(IdentityServiceDomainSharedModule),
        typeof(GuiliIdentityDomainModule),
        typeof(AbpIdentityServerDomainModule)
    )]
    public class IdentityServiceDomainModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
        }
    }
}
