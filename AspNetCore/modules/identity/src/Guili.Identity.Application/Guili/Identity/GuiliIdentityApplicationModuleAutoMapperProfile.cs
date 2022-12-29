using AutoMapper;
using Volo.Abp.Identity;

namespace Guili.Identity
{
    public class GuiliIdentityApplicationModuleAutoMapperProfile: AbpIdentityApplicationModuleAutoMapperProfile
    {
        public GuiliIdentityApplicationModuleAutoMapperProfile() : base()
        {
            CreateMap<OrganizationUnit, OrganizationUnitDto>()
            .MapExtraProperties();
        }
    }
}
