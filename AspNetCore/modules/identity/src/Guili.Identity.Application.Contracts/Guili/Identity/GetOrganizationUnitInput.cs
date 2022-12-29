using Volo.Abp.Application.Dtos;

namespace Guili.Identity
{
    public class GetOrganizationUnitInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
