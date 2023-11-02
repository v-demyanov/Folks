using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace Folks.IdentityService.Infrastructure;

public static class Config
{
    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new ApiScope(IdentityServerConstants.LocalApi.ScopeName),
            new ApiScope("movieAPI", "Movie API")
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            new Client
            {
                ClientId = "movieClient",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },
                AllowedScopes = { "movieAPI" }
            }
        };
}
