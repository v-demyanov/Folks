using Duende.IdentityServer.Events;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Services;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Folks.IdentityService.Api.Filters;

namespace Folks.IdentityService.Api.Pages.Account.Logout;

[AllowAnonymous]
[SecurityHeaders]
public class IndexModel : PageModel
{
    private readonly IIdentityServerInteractionService _identityInteractionService;
    private readonly IEventService _identityEventService;

    public IndexModel(
        IIdentityServerInteractionService identityInteractionService,
        IEventService identityEventService)
    {
        _identityInteractionService = identityInteractionService;
        _identityEventService = identityEventService;
    }

    [BindProperty]
    public string? LogoutId { get; set; }

    public async Task<IActionResult> OnGet(string logoutId)
    {
        LogoutId = logoutId;

        var isAuthenticatedUser = User.Identity is not null && User.Identity.IsAuthenticated;
        var logoutContext = await _identityInteractionService.GetLogoutContextAsync(LogoutId);
        var isShowSignoutPrompt = logoutContext is not null && logoutContext.ShowSignoutPrompt;

        if (!isAuthenticatedUser || !isShowSignoutPrompt)
        {
            return await OnPost();
        }

        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        var isAuthenticatedUser = User.Identity is not null && User.Identity.IsAuthenticated;
        if (isAuthenticatedUser)
        {
            LogoutId ??= await _identityInteractionService.CreateLogoutContextAsync();

            await HttpContext.SignOutAsync();

            await _identityEventService.RaiseAsync(new UserLogoutSuccessEvent(User.GetSubjectId(), User.GetDisplayName()));
        }

        return RedirectToPage("/Account/Logout/LoggedOut", new { logoutId = LogoutId });
    }
}
