using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;

using Duende.IdentityServer.Services;
using Duende.IdentityServer.Events;
using Duende.IdentityServer;

using Folks.IdentityService.Domain.Entities;
using Folks.IdentityService.Api.Extensions;

namespace Folks.IdentityService.Api.Pages.Account.Login;

[AllowAnonymous]
public class IndexModel : PageModel
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly IIdentityServerInteractionService _identityServerInteractionService;
    private readonly IEventService _identityServerEventService;

    public IndexModel(
        SignInManager<User> signInManager,
        IIdentityServerInteractionService identityServerInteractionService,
        IEventService identityServerEventService,
        UserManager<User> userManager)
    {
        this._signInManager = signInManager;
        this._identityServerInteractionService = identityServerInteractionService;
        this._identityServerEventService = identityServerEventService;
        this._userManager = userManager;
    }

    [BindProperty]
    public required InputModel Input { get; set; }

    [BindProperty]
    public string? ReturnUrl { get; set; }

    public IActionResult OnGet(string? returnUrl = null)
    {
        ReturnUrl = returnUrl;

        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        var context = await _identityServerInteractionService.GetAuthorizationContextAsync(ReturnUrl);

        if (ModelState.IsValid)
        {
            var signInResult = await _signInManager.PasswordSignInAsync(Input.UserName, Input.Password, false, false);
            var user = await _userManager.FindByNameAsync(Input.UserName);

            if (signInResult.Succeeded && user != null)
            {
                await _identityServerEventService.RaiseAsync(new UserLoginSuccessEvent(user.UserName, user.Id, user.UserName, clientId: context?.Client.ClientId));

                var identityServerUser = new IdentityServerUser(user.Id)
                {
                    DisplayName = user.UserName
                };

                await HttpContext.SignInAsync(identityServerUser);

                if (context != null && ReturnUrl != null)
                {
                    if (context.IsNativeClient())
                    {
                        return this.LoadingPage(ReturnUrl);
                    }

                    return Redirect(ReturnUrl);
                }

                if (Url.IsLocalUrl(ReturnUrl))
                {
                    return Redirect(ReturnUrl);
                }
                else if (string.IsNullOrEmpty(ReturnUrl))
                {
                    return Redirect("~/");
                }
                else
                {
                    throw new Exception("invalid return URL");
                }
            }

            await _identityServerEventService.RaiseAsync(new UserLoginFailureEvent(Input.UserName, "invalid credentials", clientId: context?.Client.ClientId));
        }

        return Page();
    }
}
