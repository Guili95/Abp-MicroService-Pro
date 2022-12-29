using Guili.SaasService.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Guili.SaasService;

[DependsOn(
    typeof(SaasServiceEntityFrameworkCoreTestModule)
    )]
public class SaasServiceDomainTestModule : AbpModule
{

}
