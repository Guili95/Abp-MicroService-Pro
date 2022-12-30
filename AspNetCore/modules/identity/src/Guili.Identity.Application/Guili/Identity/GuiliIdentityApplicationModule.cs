using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;

namespace Guili.Identity
{
    [DependsOn(
        typeof(GuiliIdentityDomainModule),
        typeof(GuiliIdentityApplicationContractsModule),
        typeof(AbpIdentityApplicationModule)
    )]
    public class GuiliIdentityApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<GuiliIdentityApplicationModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddProfile<GuiliIdentityApplicationModuleAutoMapperProfile>();
            });
        }
    }
}
