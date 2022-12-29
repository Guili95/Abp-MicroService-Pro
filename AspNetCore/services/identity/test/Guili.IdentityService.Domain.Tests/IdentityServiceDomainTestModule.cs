using Guili.IdentityService.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Guili.IdentityService;

[DependsOn(
    typeof(IdentityServiceEntityFrameworkCoreTestModule)
    )]
public class IdentityServiceDomainTestModule : AbpModule
{

}
