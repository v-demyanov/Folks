using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace Folks.IdentityService.Infrastructure;

public static class Config
{
    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new ApiScope(IdentityServerConstants.LocalApi.ScopeName),
        };
}
