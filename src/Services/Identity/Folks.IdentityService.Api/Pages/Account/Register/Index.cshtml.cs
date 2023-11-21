using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using AutoMapper;

using MassTransit;

using Folks.IdentityService.Domain.Entities;
using Folks.EventBus.Messages.IdentityService;

namespace Folks.IdentityService.Api.Pages.Account.Register;

[AllowAnonymous]
public class IndexModel : PageModel
{
    private readonly UserManager<User> _userManager;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IMapper _mapper;

    public IndexModel(UserManager<User> userManager, IPublishEndpoint publishEndpoint, IMapper mapper)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
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
            return await HandleRegisterSuccessAsync(user);
        }

        return HandleRegisterFailury(identityResult);
    }

    private async Task<IActionResult> HandleRegisterSuccessAsync(User user)
    {
        var userRegisteredMessage = _mapper.Map<UserRegistered>(user);
        await _publishEndpoint.Publish(userRegisteredMessage);

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
