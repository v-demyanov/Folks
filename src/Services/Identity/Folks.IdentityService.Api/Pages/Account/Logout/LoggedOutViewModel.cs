namespace Folks.IdentityService.Api.Pages.Account.Logout;

public record class LoggedOutViewModel
{
    public string? PostLogoutRedirectUri { get; init; }

    public string? ClientName { get; init; }

    public string? SignOutIframeUrl { get; init; }

    public bool AutomaticRedirectAfterSignOut { get; init; }
}
