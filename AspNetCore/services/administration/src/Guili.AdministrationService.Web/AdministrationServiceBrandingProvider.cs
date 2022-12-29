using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace Guili.AdministrationService.Web;

[Dependency(ReplaceServices = true)]
public class AdministrationServiceBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "AdministrationService";
}
