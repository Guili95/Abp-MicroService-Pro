using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace Guili.IdentityService.Web;

[Dependency(ReplaceServices = true)]
public class IdentityServiceBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "IdentityService";
}
