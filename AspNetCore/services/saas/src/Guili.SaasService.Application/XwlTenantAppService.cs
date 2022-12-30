using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using Volo.Abp.Json;
using Volo.Abp.Json.SystemTextJson;
using Volo.Abp.Settings;
using Volo.Abp.TenantManagement;

namespace Guili.SaasService
{
    public class XwlTenantAppService : TenantManagementAppServiceBase, IXwlTenantAppService
    {
        protected ITenantManager TenantManager { get; }
        protected ITenantRepository TenantRepository { get; }
        protected ISettingEncryptionService SettingEncryptionService { get; }
        protected IOptions<AbpSystemTextJsonSerializerOptions> _AbpSystemTextJsonSerializerOption { get; }
        protected IOptions<AbpJsonOptions> _AbpJsonOption { get; }

        public XwlTenantAppService(
            ITenantManager tenantManager,
            ITenantRepository tenantRepository,
            ISettingEncryptionService settingEncryptionService,
            IOptions<AbpSystemTextJsonSerializerOptions> AbpSystemTextJsonSerializerOption,
            IOptions<AbpJsonOptions> AbpJsonOption)
        {
            TenantManager = tenantManager;
            TenantRepository = tenantRepository;
            SettingEncryptionService = settingEncryptionService;
            _AbpSystemTextJsonSerializerOption = AbpSystemTextJsonSerializerOption;
            _AbpJsonOption = AbpJsonOption;
        }

        public virtual DateTime Test()
        {
            var qxd = _AbpSystemTextJsonSerializerOption.Value.JsonSerializerOptions.PropertyNamingPolicy;
            var data = _AbpJsonOption.Value;
            var xwl = SettingEncryptionService.Encrypt(new SettingDefinition("Abp.Mailing.Smtp.Password"), "uiokyxajzbgrechg");
            return DateTime.UtcNow;
        }
    }
}
