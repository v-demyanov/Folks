using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace Folks.IdentityService.Infrastructure;

public record class IdentityServerConfig
{
    public required string IssuerUri { get; init; }

    public required IEnumerable<Client> Clients { get; init; }

    public IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new ApiScope(IdentityServerConstants.LocalApi.ScopeName),
            new ApiScope("channelsServiceApi", "Channels Service API")
        };

    public IEnumerable<IdentityResource> IdentityResources =>
        new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email(),
            new IdentityResources.Phone(),
        };
}
