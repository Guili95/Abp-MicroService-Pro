using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Guili.Identity
{
    [DependsOn(
        typeof(GuiliIdentityApplicationContractsModule),
        typeof(AbpIdentityHttpApiClientModule)
    )]
    public class GuiliIdentityHttpApiClientModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddStaticHttpClientProxies(
                typeof(GuiliIdentityApplicationContractsModule).Assembly,
                IdentityRemoteServiceConsts.RemoteServiceName
            );

            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<GuiliIdentityHttpApiClientModule>();
            });
        }
    }
}
