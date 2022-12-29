using System;

namespace Guili.Identity
{
    public class OrganizationUnitCreateDto : OrganizationUnitCreateOrUpdateDtoBase
    {
        public Guid? ParentId { get; set; }
    }
}
