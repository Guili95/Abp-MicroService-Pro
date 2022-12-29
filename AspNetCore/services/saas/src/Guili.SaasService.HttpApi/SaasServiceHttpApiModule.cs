<<<<<<< HEAD
﻿using Volo.Abp.Modularity;
using Volo.Abp.TenantManagement;

namespace Guili.SaasService
{
    [DependsOn(
        typeof(SaasServiceApplicationContractsModule),
        typeof(AbpTenantManagementHttpApiModule)
    )]
    public class SaasServiceHttpApiModule : AbpModule
    {

    }
}
=======
﻿using Volo.Abp.Modularity;
using Volo.Abp.TenantManagement;

namespace Guili.SaasService
{
    [DependsOn(
        typeof(SaasServiceApplicationContractsModule),
        typeof(AbpTenantManagementHttpApiModule)
    )]
    public class SaasServiceHttpApiModule : AbpModule
    {

    }
}
>>>>>>> git/ids4
