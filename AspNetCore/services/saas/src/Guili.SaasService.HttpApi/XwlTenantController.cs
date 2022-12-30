using Microsoft.AspNetCore.Mvc;
using System;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.TenantManagement;

namespace Guili.SaasService
{
    [Controller]
    [RemoteService(Name = TenantManagementRemoteServiceConsts.RemoteServiceName)]
    [Area(TenantManagementRemoteServiceConsts.ModuleName)]
    [Route("api/xwl/multi-tenancy/tenants")]
    public class XwlTenantController : AbpControllerBase, IXwlTenantAppService
    {
        protected IXwlTenantAppService TenantManager { get; }
        public XwlTenantController(IXwlTenantAppService tenantManager)
        {
            TenantManager = tenantManager;
        }
        [HttpGet]
        public DateTime Test()
        {
            return TenantManager.Test();
        }
    }
}
