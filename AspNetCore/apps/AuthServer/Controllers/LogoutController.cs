using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Account;
using Volo.Abp.AspNetCore.Mvc;

namespace AuthServer.Controllers
{
    [RemoteService(Name = AccountRemoteServiceConsts.RemoteServiceName)]
    [Area(AccountRemoteServiceConsts.ModuleName)]
    [Route("api/account/")]
    public class LogoutController : AbpControllerBase
    {
        private readonly IIdentityServerInteractionService _interaction;

        public LogoutController(
            IIdentityServerInteractionService interaction)
        {
            _interaction = interaction;
        }

        [HttpGet]
        [Route("Logout/{logoutId}")]
        public virtual async Task<LogoutRequest> GetLogoutAsync(string logoutId)
        {
            var logoutContext = await _interaction.GetLogoutContextAsync(logoutId);

            return logoutContext;
        }
    }
}
