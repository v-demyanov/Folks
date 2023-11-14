using Duende.IdentityServer.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Folks.IdentityService.Api.Filters;
using Folks.IdentityService.Api.Constants;

namespace Folks.IdentityService.Api.Pages.Account.Logout;

[AllowAnonymous]
[SecurityHeaders]
public class LoggedOutModel : PageModel
{
    private readonly IIdentityServerInteractionService _identityInteractionService;

    public LoggedOutModel(IIdentityServerInteractionService identityInteractionService)
    {
        _identityInteractionService = identityInteractionService;
    }

    public required LoggedOutViewModel View { get; set; }

    public async Task OnGet(string logoutId)
    {
        var logoutContext = await _identityInteractionService.GetLogoutContextAsync(logoutId);

        View = new LoggedOutViewModel
        {
            AutomaticRedirectAfterSignOut = LogoutOptions.AutomaticRedirectAfterSignOut,
            PostLogoutRedirectUri = logoutContext?.PostLogoutRedirectUri,
            ClientName = string.IsNullOrEmpty(logoutContext?.ClientName) ? logoutContext?.ClientId : logoutContext?.ClientName,
            SignOutIframeUrl = logoutContext?.SignOutIFrameUrl
        };
    }
}
