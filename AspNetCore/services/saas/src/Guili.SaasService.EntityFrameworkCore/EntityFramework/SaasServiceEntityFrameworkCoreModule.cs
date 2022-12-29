<<<<<<< HEAD
﻿using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.MySQL;
using Volo.Abp.Modularity;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace Guili.SaasService.EntityFramework
{
    [DependsOn(
        typeof(SaasServiceDomainModule),
        typeof(AbpTenantManagementEntityFrameworkCoreModule),
        typeof(AbpEntityFrameworkCoreMySQLModule)
    )]
    public class SaasServiceEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<SaasServiceDbContext>(options =>
            {
                options.ReplaceDbContext<ITenantManagementDbContext>();

                options.AddDefaultRepositories(includeAllEntities: true);
            });

            Configure<AbpDbContextOptions>(options =>
            {
                options.Configure<SaasServiceDbContext>(c =>
                {
                    c.UseMySQL(b =>
                    {
                        b.MigrationsHistoryTable("__SaasService_Migrations");
                    });
                });
            });
        }
    }
}
=======
﻿using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.MySQL;
using Volo.Abp.Modularity;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace Guili.SaasService.EntityFramework
{
    [DependsOn(
        typeof(SaasServiceDomainModule),
        typeof(AbpTenantManagementEntityFrameworkCoreModule),
        typeof(AbpEntityFrameworkCoreMySQLModule)
    )]
    public class SaasServiceEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<SaasServiceDbContext>(options =>
            {
                options.ReplaceDbContext<ITenantManagementDbContext>();

                options.AddDefaultRepositories(includeAllEntities: true);
            });

            Configure<AbpDbContextOptions>(options =>
            {
                options.Configure<SaasServiceDbContext>(c =>
                {
                    c.UseMySQL(b =>
                    {
                        b.MigrationsHistoryTable("__SaasService_Migrations");
                    });
                });
            });
        }
    }
}
>>>>>>> git/ids4
