using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace Folks.IdentityService.Infrastructure.Models;

public record class IdentityServerConfig
{
    public required IEnumerable<Client> Clients { get; init; }

    public IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new ApiScope(IdentityServerConstants.LocalApi.ScopeName),
        };

    public IEnumerable<IdentityResource> IdentityResources =>
        new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };
}
