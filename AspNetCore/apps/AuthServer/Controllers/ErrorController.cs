using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Account;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Identity;
using IdentityUser = Volo.Abp.Identity.IdentityUser;

namespace AuthServer.Controllers
{
    [RemoteService(Name = AccountRemoteServiceConsts.RemoteServiceName)]
    [Area(AccountRemoteServiceConsts.ModuleName)]
    [Route("api/account")]
    public class ErrorController : AbpControllerBase
    {
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IAntiforgery _antiforgery;
        private readonly SignInManager<IdentityUser> SignInManager;
        private readonly IdentityUserManager UserManager;
        private readonly IdentitySecurityLogManager IdentitySecurityLogManager;

        public ErrorController(
            SignInManager<IdentityUser> signInManager,
            IdentityUserManager userManager,
            IdentitySecurityLogManager identitySecurityLogManager,
            IIdentityServerInteractionService interaction,
            IAntiforgery antiforgery)
        {
            SignInManager = signInManager;
            UserManager = userManager;
            IdentitySecurityLogManager = identitySecurityLogManager;
            _interaction = interaction;
            _antiforgery = antiforgery;
        }

        [HttpGet]
        [Route("Error/{errorId}")]
        public virtual async Task<ErrorMessage> GetErrorAsync(string errorId)
        {
            var errorMessage = await _interaction.GetErrorContextAsync(errorId) ?? new ErrorMessage
            {
                Error = L["Error"]
            };

            return errorMessage;
        }
    }
}
