using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Guili.Identity
{
    public class OrganizationUnitDto : ExtensibleFullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public Guid? ParentId { get; set; }

        public string Code { get; set; }

        public string DisplayName { get; set; }
        public string ConcurrencyStamp { get; set; }
    }
}
