<<<<<<< HEAD
﻿using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Identity;

namespace Guili.Identity
{
    public interface IOrganizationUnitAppService : IApplicationService
    {
        Task<OrganizationUnitDto> GetAsync(Guid id);
        Task<PagedResultDto<OrganizationUnitDto>> GetListAsync(GetOrganizationUnitInput input);
        Task<ListResultDto<OrganizationUnitDto>> GetListAllAsync();
        Task<OrganizationUnitDto> CreateAsync(OrganizationUnitCreateDto input);
        Task<OrganizationUnitDto> UpdateAsync(Guid id, OrganizationUnitUpdateDto input);
        Task MoveAsync(Guid id, OrganizationUnitMoveInput input);
        Task DeleteAsync(Guid id);
        Task<PagedResultDto<IdentityUserDto>> GetMembersAsync(Guid id, GetIdentityUsersInput input);
        Task<ListResultDto<Guid>> GetMembersIdAllAsync(Guid id);
        Task AddMembersAsync(Guid id, AddMemberToOrganizationUnitInput input);
        Task RemoveMemberAsync(Guid id, Guid userId);
        Task<PagedResultDto<IdentityRoleDto>> GetRolesAsync(Guid id, PagedAndSortedResultRequestDto input);
        Task AddRolesAsync(Guid id, OrganizationUnitRoleInput input);
        Task<ListResultDto<Guid>> GetRolesIdAllAsync(Guid id);
        Task RemoveRoleAsync(Guid id, Guid roleId);
    }
}
=======
﻿using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Identity;

namespace Guili.Identity
{
    public interface IOrganizationUnitAppService : IApplicationService
    {
        Task<OrganizationUnitDto> GetAsync(Guid id);
        Task<PagedResultDto<OrganizationUnitDto>> GetListAsync(GetOrganizationUnitInput input);
        Task<ListResultDto<OrganizationUnitDto>> GetListAllAsync();
        Task<OrganizationUnitDto> CreateAsync(OrganizationUnitCreateDto input);
        Task<OrganizationUnitDto> UpdateAsync(Guid id, OrganizationUnitUpdateDto input);
        Task MoveAsync(Guid id, OrganizationUnitMoveInput input);
        Task DeleteAsync(Guid id);
        Task<PagedResultDto<IdentityUserDto>> GetMembersAsync(Guid id, GetIdentityUsersInput input);
        Task<ListResultDto<Guid>> GetMembersIdAllAsync(Guid id);
        Task AddMembersAsync(Guid id, AddMemberToOrganizationUnitInput input);
        Task RemoveMemberAsync(Guid id, Guid userId);
        Task<PagedResultDto<IdentityRoleDto>> GetRolesAsync(Guid id, PagedAndSortedResultRequestDto input);
        Task AddRolesAsync(Guid id, OrganizationUnitRoleInput input);
        Task<ListResultDto<Guid>> GetRolesIdAllAsync(Guid id);
        Task RemoveRoleAsync(Guid id, Guid roleId);
    }
}
>>>>>>> git/ids4
