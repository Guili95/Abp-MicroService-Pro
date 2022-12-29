using Guili.Identity;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace Guili.IdentityService
{
    [DependsOn(
        typeof(IdentityServiceDomainModule),
        typeof(IdentityServiceApplicationContractsModule),
        typeof(GuiliIdentityApplicationModule)
    )]
    public class IdentityServiceApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<IdentityServiceApplicationModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<IdentityServiceApplicationModule>(validate: true);
            });
        }
    }
}
