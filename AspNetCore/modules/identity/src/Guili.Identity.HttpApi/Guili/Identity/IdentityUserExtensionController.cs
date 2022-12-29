using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Identity;

namespace Guili.Identity
{
    [RemoteService(Name = IdentityRemoteServiceConsts.RemoteServiceName)]
    [Area(IdentityRemoteServiceConsts.ModuleName)]
    [ControllerName("User")]
    [Route("api/identity/users")]
    public class IdentityUserExtensionController : AbpControllerBase, IIdentityUserExtensionAppService
    {
        protected IIdentityUserExtensionAppService UserExtensionAppService { get; }

        public IdentityUserExtensionController(IIdentityUserExtensionAppService userExtensionAppService)
        {
            UserExtensionAppService = userExtensionAppService;
        }

        [HttpPut]
        [Route("{id}/organization-units")]
        public Task UpdateOrganizationUnitsAsync(Guid id, IdentityUserUpdateOrganizationUnitDto input)
        {
            return UserExtensionAppService.UpdateOrganizationUnitsAsync(id, input);
        }

        [HttpGet]
        [Route("{id}/organization-units")]
        public virtual Task<List<OrganizationUnitDto>> GetOrganizationUnitsAsync(Guid id)
        {
            return UserExtensionAppService.GetOrganizationUnitsAsync(id);
        }
    }
}
