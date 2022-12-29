<<<<<<< HEAD
﻿using Volo.Abp.Modularity;
using Volo.Abp.TenantManagement;

namespace Guili.SaasService
{
    [DependsOn(
        typeof(SaasServiceDomainSharedModule),
        typeof(AbpTenantManagementDomainModule)
    )]
    public class SaasServiceDomainModule : AbpModule
    {
    }
}
=======
﻿using Volo.Abp.Modularity;
using Volo.Abp.TenantManagement;

namespace Guili.SaasService
{
    [DependsOn(
        typeof(SaasServiceDomainSharedModule),
        typeof(AbpTenantManagementDomainModule)
    )]
    public class SaasServiceDomainModule : AbpModule
    {
    }
}
>>>>>>> git/ids4
