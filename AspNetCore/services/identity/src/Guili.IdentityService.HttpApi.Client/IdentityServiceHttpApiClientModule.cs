using Guili.Identity;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace Guili.IdentityService
{
    [DependsOn(
        typeof(IdentityServiceApplicationContractsModule),
        typeof(GuiliIdentityHttpApiClientModule)
    )]
    public class IdentityServiceHttpApiClientModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(IdentityServiceApplicationContractsModule).Assembly,
                IdentityServiceRemoteServiceConsts.RemoteServiceName
            );
        }
    }
}
