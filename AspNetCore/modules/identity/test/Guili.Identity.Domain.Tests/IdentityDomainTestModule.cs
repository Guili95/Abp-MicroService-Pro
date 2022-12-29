using Guili.Identity.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Guili.Identity;

[DependsOn(
    typeof(IdentityEntityFrameworkCoreTestModule)
    )]
public class IdentityDomainTestModule : AbpModule
{

}
