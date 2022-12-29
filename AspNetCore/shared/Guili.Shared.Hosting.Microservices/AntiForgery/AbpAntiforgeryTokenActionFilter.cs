<<<<<<< HEAD
﻿using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.AntiForgery;
using Volo.Abp.DependencyInjection;

namespace Guili.Shared.Hosting.Microservices.AntiForgery
{
    public class AbpAntiforgeryTokenActionFilter : IAsyncActionFilter, ITransientDependency
    {
        private readonly IAbpAntiForgeryManager _antiForgeryManager;
        public AbpAntiforgeryTokenActionFilter(IAbpAntiForgeryManager antiForgeryManager)
        {
            _antiForgeryManager = antiForgeryManager;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.RouteData.Values["action"].ToString() == "Logout")
            {
                context.HttpContext.User = new System.Security.Principal.GenericPrincipal(new System.Security.Principal.GenericIdentity(string.Empty), null);
            }
            if (!context.Controller.ToString().Contains("AbpApplicationConfigurationController") && !context.Controller.ToString().Contains("AbpApplicationConfigurationScriptController") && !context.Controller.ToString().Contains("AbpSwashbuckleController"))
            {
                _antiForgeryManager.SetCookie();
            }
            await next.Invoke();
        }
    }
}
=======
﻿using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.AntiForgery;
using Volo.Abp.DependencyInjection;

namespace Guili.Shared.Hosting.Microservices.AntiForgery
{
    public class AbpAntiforgeryTokenActionFilter : IAsyncActionFilter, ITransientDependency
    {
        private readonly IAbpAntiForgeryManager _antiForgeryManager;
        public AbpAntiforgeryTokenActionFilter(IAbpAntiForgeryManager antiForgeryManager)
        {
            _antiForgeryManager = antiForgeryManager;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.RouteData.Values["action"].ToString() == "Logout")
            {
                context.HttpContext.User = new System.Security.Principal.GenericPrincipal(new System.Security.Principal.GenericIdentity(string.Empty), null);
            }
            if (!context.Controller.ToString().Contains("AbpApplicationConfigurationController") && !context.Controller.ToString().Contains("AbpApplicationConfigurationScriptController") && !context.Controller.ToString().Contains("AbpSwashbuckleController"))
            {
                _antiForgeryManager.SetCookie();
            }
            await next.Invoke();
        }
    }
}
>>>>>>> git/ids4
