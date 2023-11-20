using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Folks.IdentityService.Domain.Entities;

namespace Folks.IdentityService.Api.Pages.Account.Register;

[AllowAnonymous]
public class IndexModel : PageModel
{
    private readonly UserManager<User> _userManager;

    public IndexModel(UserManager<User> userManager)
    {
        _userManager = userManager;
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

        var user = new User
        {
            UserName = Input.UserName,
            Email = Input.Email,
        };
        var identityResult = await _userManager.CreateAsync(user, Input.Password);

        if (identityResult.Succeeded)
        {
            return HandleRegisterSuccess();
        }

        return HandleRegisterFailury(identityResult);
    }

    private IActionResult HandleRegisterSuccess()
    {
        return RedirectToPage("/Account/Login/Index", new
        {
            returnUrl = ReturnUrl
        });
    }

    private IActionResult HandleRegisterFailury(IdentityResult identityResult)
    {
        AddErrors(identityResult);

        return Page();
    }

    private void AddErrors(IdentityResult identityResult)
    {
        foreach (var error in identityResult.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }
    }
}
