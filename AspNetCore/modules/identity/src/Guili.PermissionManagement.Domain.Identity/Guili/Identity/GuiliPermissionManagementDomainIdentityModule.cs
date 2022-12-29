using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.PermissionManagement.Identity;

namespace Guili.Identity
{
    [DependsOn(
        typeof(GuiliIdentityDomainSharedModule),
        typeof(AbpPermissionManagementDomainIdentityModule),
        typeof(GuiliIdentityApplicationContractsModule)
    )]
    public class GuiliPermissionManagementDomainIdentityModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<PermissionManagementOptions>(options =>
            {
                options.ManagementProviders.Add<UserPermissionManagementProvider>();
                options.ManagementProviders.Add<RolePermissionManagementProvider>();

                options.ProviderPolicies[RolePermissionValueProvider.ProviderName] = "AbpIdentity.Roles.ManagePermissions";
                options.ProviderPolicies[UserPermissionValueProvider.ProviderName] = "AbpIdentity.Users.ManagePermissions";
            });
        }
    }
}
