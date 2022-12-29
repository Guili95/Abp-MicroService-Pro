using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace Guili.SaasService.Web;

[Dependency(ReplaceServices = true)]
public class SaasServiceBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "SaasService";
}
