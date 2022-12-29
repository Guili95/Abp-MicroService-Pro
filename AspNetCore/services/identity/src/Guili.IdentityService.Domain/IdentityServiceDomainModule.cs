using Guili.Identity;
using Volo.Abp.Identity;
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
    }
}
