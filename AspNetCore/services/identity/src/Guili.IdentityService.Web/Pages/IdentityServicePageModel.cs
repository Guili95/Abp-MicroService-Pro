using Guili.IdentityService.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Guili.IdentityService.Web.Pages;

public abstract class IdentityServicePageModel : AbpPageModel
{
    protected IdentityServicePageModel()
    {
        LocalizationResourceType = typeof(IdentityServiceResource);
    }
}
