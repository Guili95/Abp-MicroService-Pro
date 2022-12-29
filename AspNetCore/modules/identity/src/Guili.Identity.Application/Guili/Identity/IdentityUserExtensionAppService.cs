using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Identity;
using AbpIdentity = Volo.Abp.Identity;

namespace Guili.Identity
{
    [Authorize(AbpIdentity.IdentityPermissions.Users.Default)]
    public class IdentityUserExtensionAppService : IdentityAppServiceBase, IIdentityUserExtensionAppService
    {
        protected IdentityUserManager UserManager { get; }
        protected IIdentityUserRepository UserRepository { get; }
        public IdentityUserExtensionAppService(
            IdentityUserManager userManager,
            IIdentityUserRepository userRepository)
        {
            UserManager = userManager;
            UserRepository = userRepository;
        }

        [Authorize(AbpIdentity.IdentityPermissions.Users.Update)]
        public virtual async Task UpdateOrganizationUnitsAsync(Guid id, IdentityUserUpdateOrganizationUnitDto input)
        {
            await UserManager.SetOrganizationUnitsAsync(id, input.OrganizationUnitIds);
        }

        public virtual async Task<List<OrganizationUnitDto>> GetOrganizationUnitsAsync(Guid id)
        {
            var organizationUnits = await UserRepository.GetOrganizationUnitsAsync(id, includeDetails: true);
            return new List<OrganizationUnitDto>(
                ObjectMapper.Map<List<OrganizationUnit>, List<OrganizationUnitDto>>(organizationUnits)
            );
        }
    }
}
