using Volo.Abp.Identity;
using Volo.Abp.Modularity;

namespace Guili.Identity
{
    [DependsOn(
        typeof(AbpIdentityDomainModule)
    )]
    public class GuiliIdentityDomainModule : AbpModule
    {
    }
}

