﻿using Volo.Abp.AuditLogging;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.Identity;
using Volo.Abp.PermissionManagement.IdentityServer;
using Volo.Abp.SettingManagement;

namespace Guili.AdministrationService
{
    [DependsOn(
        typeof(AdministrationServiceDomainSharedModule),
        typeof(AbpSettingManagementDomainModule),
        typeof(AbpAuditLoggingDomainModule),
        typeof(AbpFeatureManagementDomainModule),
        typeof(AbpPermissionManagementDomainIdentityModule),
        typeof(AbpPermissionManagementDomainIdentityServerModule)
    )]
    public class AdministrationServiceDomainModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Languages.Add(new LanguageInfo("en", "en", "English", "gb"));
                options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "简体中文", "cn"));
            });
        }
    }
}
