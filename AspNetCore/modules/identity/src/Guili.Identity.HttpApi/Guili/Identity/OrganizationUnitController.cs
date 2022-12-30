using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Identity;

namespace Guili.Identity
{
    [RemoteService(Name = IdentityRemoteServiceConsts.RemoteServiceName)]
    [Area(IdentityRemoteServiceConsts.ModuleName)]
    [ControllerName("OrganizationUnit")]
    [Route("api/identity/organization-units")]
    public class OrganizationUnitController : AbpControllerBase, IOrganizationUnitAppService
    {
        protected IOrganizationUnitAppService OrganizationUnitAppService { get; }

        public OrganizationUnitController(IOrganizationUnitAppService organizationUnitAppService)
        {
            OrganizationUnitAppService = organizationUnitAppService;
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<OrganizationUnitDto> GetAsync(Guid id)
        {
            return OrganizationUnitAppService.GetAsync(id);
        }

        [HttpGet]
        public virtual Task<PagedResultDto<OrganizationUnitDto>> GetListAsync(GetOrganizationUnitInput input)
        {
            return OrganizationUnitAppService.GetListAsync(input);
        }
        [HttpGet]
        [Route("all")]
        public virtual Task<ListResultDto<OrganizationUnitDto>> GetListAllAsync()
        {
            return OrganizationUnitAppService.GetListAllAsync();
        }

        [HttpPost]
        public virtual Task<OrganizationUnitDto> CreateAsync(OrganizationUnitCreateDto input)
        {
            return OrganizationUnitAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<OrganizationUnitDto> UpdateAsync(Guid id, OrganizationUnitUpdateDto input)
        {
            return OrganizationUnitAppService.UpdateAsync(id, input);
        }

        [HttpPut]
        [Route("{id}/move")]
        public virtual Task MoveAsync(Guid id, OrganizationUnitMoveInput input)
        {
            return OrganizationUnitAppService.MoveAsync(id, input);
        }

        [HttpDelete]
        public virtual Task DeleteAsync(Guid id)
        {
            return OrganizationUnitAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("{id}/members")]
        public virtual Task<PagedResultDto<IdentityUserDto>> GetMembersAsync(Guid id, GetIdentityUsersInput input)
        {
            return OrganizationUnitAppService.GetMembersAsync(id, input);
        }

        [HttpGet]
        [Route("{id}/membersidall")]
        public Task<ListResultDto<Guid>> GetMembersIdAllAsync(Guid id)
        {
            return OrganizationUnitAppService.GetMembersIdAllAsync(id);
        }

        [HttpPut]
        [Route("{id}/members")]
        public virtual Task AddMembersAsync(Guid id, AddMemberToOrganizationUnitInput input)
        {
            return OrganizationUnitAppService.AddMembersAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}/members/{userId}")]
        public Task RemoveMemberAsync(Guid id, Guid userId)
        {
            return OrganizationUnitAppService.RemoveMemberAsync(id, userId);
        }

        [HttpGet]
        [Route("{id}/roles")]
        public virtual Task<PagedResultDto<IdentityRoleDto>> GetRolesAsync(Guid id, PagedAndSortedResultRequestDto input)
        {
            return OrganizationUnitAppService.GetRolesAsync(id, input);
        }

        [HttpGet]
        [Route("{id}/rolesidall")]
        public Task<ListResultDto<Guid>> GetRolesIdAllAsync(Guid id)
        {
            return OrganizationUnitAppService.GetRolesIdAllAsync(id);
        }

        [HttpPut]
        [Route("{id}/roles")]
        public Task AddRolesAsync(Guid id, OrganizationUnitRoleInput input)
        {
            return OrganizationUnitAppService.AddRolesAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}/roles/{roleId}")]
        public Task RemoveRoleAsync(Guid id, Guid roleId)
        {
            return OrganizationUnitAppService.RemoveRoleAsync(id, roleId);
        }
    }
}
