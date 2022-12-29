using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Guili.Identity
{
    public interface IIdentityUserExtensionAppService : IApplicationService
    {
        Task UpdateOrganizationUnitsAsync(Guid id, IdentityUserUpdateOrganizationUnitDto input);
        Task<List<OrganizationUnitDto>> GetOrganizationUnitsAsync(Guid id);
    }
}
