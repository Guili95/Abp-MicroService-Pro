using Guili.AdministrationService.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Guili.AdministrationService;

[DependsOn(
    typeof(AdministrationServiceEntityFrameworkCoreTestModule)
    )]
public class AdministrationServiceDomainTestModule : AbpModule
{

}
