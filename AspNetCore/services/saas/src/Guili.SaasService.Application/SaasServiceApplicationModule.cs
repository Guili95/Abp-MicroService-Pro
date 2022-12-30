﻿using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.TenantManagement;

namespace Guili.SaasService
{
    [DependsOn(
       typeof(SaasServiceApplicationContractsModule),
       typeof(SaasServiceDomainModule),
       typeof(AbpTenantManagementApplicationModule)
    )]
    public class SaasServiceApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<SaasServiceApplicationModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<SaasServiceApplicationModule>(validate: true);
            });
        }
    }
}
