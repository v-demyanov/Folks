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
using Folks.IdentityService.Api.Filters;
using Folks.IdentityService.Api.Constants;

namespace Folks.IdentityService.Api.Pages.Account.Login;

[AllowAnonymous]
[SecurityHeaders]
public class IndexModel : PageModel
{
    private readonly SignInManager<User> _signInManager;
    private readonly IIdentityServerInteractionService _identityInteractionService;
    private readonly IEventService _identityEventService;

    public IndexModel(
        SignInManager<User> signInManager,
        IIdentityServerInteractionService identityInteractionService,
        IEventService identityEventService)
    {
        _signInManager = signInManager;
        _identityInteractionService = identityInteractionService;
        _identityEventService = identityEventService;
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
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var signInResult = await _signInManager.PasswordSignInAsync(Input.UserName, Input.Password, false, false);
        var user = await _signInManager.UserManager.FindByNameAsync(Input.UserName);

        if (signInResult.Succeeded && user is not null)
        {
            return await HandleLoginSuccess(user);
        }

        return await HandleLoginFailure();
    }

    private async Task<IActionResult> HandleLoginFailure()
    {
        var authorizationContext = await _identityInteractionService.GetAuthorizationContextAsync(ReturnUrl);
        await _identityEventService.RaiseAsync(new UserLoginFailureEvent(Input.UserName, LoginErrorMessages.InvalidCredentialsErrorMessage)
        {
            ClientId = authorizationContext?.Client.ClientId
        });

        ModelState.AddModelError(string.Empty, LoginErrorMessages.InvalidCredentialsErrorMessage);

        return Page();
    }

    private async Task<IActionResult> HandleLoginSuccess(User user)
    {
        var authorizationContext = await _identityInteractionService.GetAuthorizationContextAsync(ReturnUrl);
        if (authorizationContext is null || ReturnUrl is null)
        {
            return HandleLoginBoundaryCases();
        }

        await _identityEventService.RaiseAsync(new UserLoginSuccessEvent(user.UserName, user.Id, user.UserName)
        {
            ClientId = authorizationContext.Client.ClientId
        });

        await HttpContext.SignInAsync(new IdentityServerUser(user.Id)
        {
            DisplayName = user.UserName
        });

        if (authorizationContext.IsNativeClient())
        {
            return this.LoadingPage(ReturnUrl);
        }

        return Redirect(ReturnUrl);
    }

    private IActionResult HandleLoginBoundaryCases()
    {
        if (Url.IsLocalUrl(ReturnUrl))
        {
            return Redirect(ReturnUrl);
        }
        else if (string.IsNullOrEmpty(ReturnUrl))
        {
            return Redirect("~/");
        }

        throw new Exception(LoginErrorMessages.InvalidReturnUrl);
    }
}
