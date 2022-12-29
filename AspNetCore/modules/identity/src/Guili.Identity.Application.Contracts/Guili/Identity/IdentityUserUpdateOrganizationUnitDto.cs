using System;
using System.ComponentModel.DataAnnotations;

namespace Guili.Identity
{
    public class IdentityUserUpdateOrganizationUnitDto
    {
        [Required]
        public Guid[] OrganizationUnitIds { get; set; }
    }
}
