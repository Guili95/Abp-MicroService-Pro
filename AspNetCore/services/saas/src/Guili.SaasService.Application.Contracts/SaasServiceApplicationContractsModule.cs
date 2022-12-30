using Volo.Abp.Modularity;
using Volo.Abp.TenantManagement;

namespace Guili.SaasService
{
    [DependsOn(
        typeof(AbpTenantManagementApplicationContractsModule),
        typeof(SaasServiceDomainSharedModule)
    )]
    public class SaasServiceApplicationContractsModule : AbpModule
    {
    }
}
