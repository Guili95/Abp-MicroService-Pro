using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Data;
using Volo.Abp.Identity;
using Volo.Abp.ObjectExtending;

namespace Guili.Identity
{
    //[Authorize(IdentityPermissions.OrganizationUnits.Default)]
    public class OrganizationUnitAppService : IdentityAppServiceBase, IOrganizationUnitAppService
    {
        protected OrganizationUnitManager OrganizationUnitManager { get; }
        protected IOrganizationUnitRepository OrganizationUnitRepository { get; }
        protected IdentityUserManager IdentityUserManager { get; }

        public OrganizationUnitAppService(
            OrganizationUnitManager organizationUnitManager,
            IOrganizationUnitRepository organizationUnitRepository,
            IdentityUserManager identityUserManager)
        {
            OrganizationUnitManager = organizationUnitManager;
            OrganizationUnitRepository = organizationUnitRepository;
            IdentityUserManager = identityUserManager;
        }

        public virtual async Task<OrganizationUnitDto> GetAsync(Guid id)
        {
            var organizationUnit = await OrganizationUnitRepository.GetAsync(id);
            return ObjectMapper.Map<OrganizationUnit, OrganizationUnitDto>(organizationUnit);
        }

        public virtual async Task<PagedResultDto<OrganizationUnitDto>> GetListAsync(GetOrganizationUnitInput input)
        {
            var organizationUnits = await OrganizationUnitRepository.GetListAsync(
                sorting: input.Sorting,
                maxResultCount: input.MaxResultCount,
                skipCount: input.SkipCount,
                includeDetails: true
            );

            return new PagedResultDto<OrganizationUnitDto>(
                await OrganizationUnitRepository.GetCountAsync(),
                ObjectMapper.Map<List<OrganizationUnit>, List<OrganizationUnitDto>>(organizationUnits)
            );
        }

        public virtual async Task<ListResultDto<OrganizationUnitDto>> GetListAllAsync()
        {
            var organizationUnits = await OrganizationUnitRepository.GetListAsync(includeDetails: true);

            return new ListResultDto<OrganizationUnitDto>(
                ObjectMapper.Map<List<OrganizationUnit>, List<OrganizationUnitDto>>(organizationUnits)
            );
        }
        public virtual async Task<OrganizationUnitDto> CreateAsync(OrganizationUnitCreateDto input)
        {
            var organizationUnit = new OrganizationUnit(
                GuidGenerator.Create(),
                input.DisplayName,
                input.ParentId,
                CurrentTenant.Id
            );

            input.MapExtraPropertiesTo(organizationUnit);

            await OrganizationUnitManager.CreateAsync(organizationUnit);

            return ObjectMapper.Map<OrganizationUnit, OrganizationUnitDto>(organizationUnit);
        }

        public virtual async Task<OrganizationUnitDto> UpdateAsync(Guid id, OrganizationUnitUpdateDto input)
        {
            var organizationUnit = await OrganizationUnitRepository.GetAsync(id);

            organizationUnit.DisplayName = input.DisplayName;
            organizationUnit.SetConcurrencyStampIfNotNull(input.ConcurrencyStamp);

            input.MapExtraPropertiesTo(organizationUnit);

            await OrganizationUnitManager.UpdateAsync(organizationUnit);
            await CurrentUnitOfWork.SaveChangesAsync();

            return ObjectMapper.Map<OrganizationUnit, OrganizationUnitDto>(organizationUnit);
        }

        public virtual async Task MoveAsync(Guid id, OrganizationUnitMoveInput input)
        {
            await OrganizationUnitManager.MoveAsync(id, input.NewParentId);
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            var ou = await OrganizationUnitRepository.FindAsync(id);
            if (ou == null)
            {
                return;
            }

            await OrganizationUnitManager.DeleteAsync(id);
        }

        public virtual async Task<PagedResultDto<IdentityUserDto>> GetMembersAsync(Guid id, GetIdentityUsersInput input)
        {
            var ou = await OrganizationUnitRepository.GetAsync(id);
            var count = await OrganizationUnitRepository.GetMembersCountAsync(ou, input.Filter);
            var users = await OrganizationUnitRepository.GetMembersAsync(ou, input.Sorting, input.MaxResultCount,
                input.SkipCount, input.Filter);
            return new PagedResultDto<IdentityUserDto>
            {
                Items = ObjectMapper.Map<List<IdentityUser>, List<IdentityUserDto>>(users),
                TotalCount = count
            };
        }

        public virtual async Task<ListResultDto<Guid>> GetMembersIdAllAsync(Guid id)
        {
            var ou = await OrganizationUnitRepository.GetAsync(id);
            var users = await OrganizationUnitRepository.GetMembersAsync(ou);
            return new ListResultDto<Guid>
            {
                Items = users.Select(a => a.Id).ToList()
            };
        }

        public virtual async Task AddMembersAsync(Guid id, AddMemberToOrganizationUnitInput input)
        {
            foreach (var userId in input.UserIds)
            {
                await IdentityUserManager.AddToOrganizationUnitAsync(userId, id);
            }
        }

        public virtual async Task RemoveMemberAsync(Guid id, Guid userId)
        {
            await IdentityUserManager.RemoveFromOrganizationUnitAsync(userId, id);
        }

        public virtual async Task<PagedResultDto<IdentityRoleDto>> GetRolesAsync(Guid id, PagedAndSortedResultRequestDto input)
        {
            var ou = await OrganizationUnitRepository.GetAsync(id);
            var roles = await OrganizationUnitRepository.GetRolesAsync(ou, input.Sorting, input.MaxResultCount,
                input.SkipCount);
            var count = await OrganizationUnitRepository.GetRolesCountAsync(ou);
            return new PagedResultDto<IdentityRoleDto>
            {
                Items = ObjectMapper.Map<List<IdentityRole>, List<IdentityRoleDto>>(roles),
                TotalCount = count
            };
        }

        public virtual async Task AddRolesAsync(Guid id, OrganizationUnitRoleInput input)
        {
            foreach (var roleId in input.RoleIds)
            {
                await OrganizationUnitManager.AddRoleToOrganizationUnitAsync(roleId, id);
            }
        }
        public virtual async Task<ListResultDto<Guid>> GetRolesIdAllAsync(Guid id)
        {
            var ou = await OrganizationUnitRepository.GetAsync(id);
            var roles = await OrganizationUnitRepository.GetRolesAsync(ou);
            return new ListResultDto<Guid>
            {
                Items = roles.Select(a => a.Id).ToList()
            };
        }

        public virtual async Task RemoveRoleAsync(Guid id, Guid roleId)
        {
            await OrganizationUnitManager.RemoveRoleFromOrganizationUnitAsync(roleId, id);
        }
    }
}
