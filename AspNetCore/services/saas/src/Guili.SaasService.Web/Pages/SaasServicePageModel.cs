using Guili.SaasService.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Guili.SaasService.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class SaasServicePageModel : AbpPageModel
{
    protected SaasServicePageModel()
    {
        LocalizationResourceType = typeof(SaasServiceResource);
    }
}
