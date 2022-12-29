<<<<<<< HEAD
﻿using Volo.Abp.Identity;
using Volo.Abp.Modularity;

namespace Guili.Identity
{
    [DependsOn(
        typeof(GuiliIdentityDomainSharedModule),
        typeof(AbpIdentityApplicationContractsModule)    
    )]
    public class GuiliIdentityApplicationContractsModule : AbpModule
    {
    }
}
=======
﻿using Volo.Abp.Identity;
using Volo.Abp.Modularity;

namespace Guili.Identity
{
    [DependsOn(
        typeof(GuiliIdentityDomainSharedModule),
        typeof(AbpIdentityApplicationContractsModule)    
    )]
    public class GuiliIdentityApplicationContractsModule : AbpModule
    {
    }
}
>>>>>>> git/ids4
