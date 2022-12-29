using Guili.AdministrationService.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Guili.AdministrationService.Web.Pages;

public abstract class AdministrationServicePageModel : AbpPageModel
{
    protected AdministrationServicePageModel()
    {
        LocalizationResourceType = typeof(AdministrationServiceResource);
    }
}
