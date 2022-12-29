<<<<<<< HEAD
﻿using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.MySQL;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;

namespace Guili.AdministrationService.EntityFrameworkCore
{
    [DependsOn(
        typeof(AdministrationServiceDomainModule),
        typeof(AbpEntityFrameworkCoreMySQLModule),
        typeof(AbpPermissionManagementEntityFrameworkCoreModule),
        typeof(AbpSettingManagementEntityFrameworkCoreModule),
        typeof(AbpAuditLoggingEntityFrameworkCoreModule),
        typeof(AbpFeatureManagementEntityFrameworkCoreModule)
    )]
    public class AdministrationServiceEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<AdministrationServiceDbContext>(options =>
            {
                options.ReplaceDbContext<IPermissionManagementDbContext>();
                options.ReplaceDbContext<ISettingManagementDbContext>();
                options.ReplaceDbContext<IAuditLoggingDbContext>();
                options.ReplaceDbContext<IFeatureManagementDbContext>();

                options.AddDefaultRepositories(includeAllEntities: true);
            });

            Configure<AbpDbContextOptions>(options =>
            {
                options.Configure<AdministrationServiceDbContext>(c =>
                {
                    c.UseMySQL(b =>
                    {
                        b.MigrationsHistoryTable("__AdministrationService_Migrations");
                    });
                });
            });
        }
    }
}
=======
﻿using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.MySQL;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;

namespace Guili.AdministrationService.EntityFrameworkCore
{
    [DependsOn(
        typeof(AdministrationServiceDomainModule),
        typeof(AbpEntityFrameworkCoreMySQLModule),
        typeof(AbpPermissionManagementEntityFrameworkCoreModule),
        typeof(AbpSettingManagementEntityFrameworkCoreModule),
        typeof(AbpAuditLoggingEntityFrameworkCoreModule),
        typeof(AbpFeatureManagementEntityFrameworkCoreModule)
    )]
    public class AdministrationServiceEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<AdministrationServiceDbContext>(options =>
            {
                options.ReplaceDbContext<IPermissionManagementDbContext>();
                options.ReplaceDbContext<ISettingManagementDbContext>();
                options.ReplaceDbContext<IAuditLoggingDbContext>();
                options.ReplaceDbContext<IFeatureManagementDbContext>();

                options.AddDefaultRepositories(includeAllEntities: true);
            });

            Configure<AbpDbContextOptions>(options =>
            {
                options.Configure<AdministrationServiceDbContext>(c =>
                {
                    c.UseMySQL(b =>
                    {
                        b.MigrationsHistoryTable("__AdministrationService_Migrations");
                    });
                });
            });
        }
    }
}
>>>>>>> git/ids4
