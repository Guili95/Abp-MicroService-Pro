using Guili.Identity;
using Guili.IdentityService.Localization;
using Volo.Abp.IdentityServer;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;

namespace Guili.IdentityService
{

    [DependsOn(
        typeof(GuiliIdentityDomainSharedModule),
        typeof(AbpIdentityServerDomainSharedModule)
    )]
    public class IdentityServiceDomainSharedModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<IdentityServiceDomainSharedModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<IdentityServiceResource>("zh-Hans")
                    .AddBaseTypes(typeof(AbpValidationResource))
                    .AddVirtualJson("/Localization/IdentityService");

                options.DefaultResourceType = typeof(IdentityServiceResource);
            });
        }
    }
}
