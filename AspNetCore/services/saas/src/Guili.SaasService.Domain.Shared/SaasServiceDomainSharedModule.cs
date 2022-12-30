using Volo.Abp.Modularity;
using Volo.Abp.TenantManagement;

namespace Guili.SaasService
{
    [DependsOn(
        typeof(AbpTenantManagementDomainSharedModule)
    )]
    public class SaasServiceDomainSharedModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {

        }
    }
}
