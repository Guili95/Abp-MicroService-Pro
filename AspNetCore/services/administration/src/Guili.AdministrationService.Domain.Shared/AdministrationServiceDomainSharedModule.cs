using Guili.AdministrationService.Localization;
using Volo.Abp.AuditLogging;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;
using Volo.Abp.FeatureManagement;

namespace Guili.AdministrationService
{
    [DependsOn(
        typeof(AbpPermissionManagementDomainSharedModule),
        typeof(AbpSettingManagementDomainSharedModule),
        typeof(AbpAuditLoggingDomainSharedModule),
        typeof(AbpFeatureManagementDomainSharedModule)
    )]
    public class AdministrationServiceDomainSharedModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            //Configure<AbpVirtualFileSystemOptions>(options =>
            //{
            //    options.FileSets.AddEmbedded<AdministrationServiceDomainSharedModule>();
            //});

            //Configure<AbpLocalizationOptions>(options =>
            //{
            //    options.Resources
            //        .Add<AdministrationServiceResource>("zh-Hans")
            //        .AddBaseTypes(typeof(AbpValidationResource))
            //        .AddVirtualJson("/Localization/AdministrationService");

            //    options.DefaultResourceType = typeof(AdministrationServiceResource);
            //});

            //Configure<AbpExceptionLocalizationOptions>(options =>
            //{
            //    options.MapCodeNamespace("AdministrationService", typeof(AdministrationServiceResource));
            //});
        }
    }
}
