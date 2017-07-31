using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace IdentityServerPlayground
{
    public class Configuration
    {
        internal static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("api_playground_1", "Playground API #1"),
                new ApiResource("api_playground_2", "Playground API #2")
            };
        }


        internal static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "WebSite1", // This needs to match the site thats trying to authenticate
                    ClientName = "Basic MVC Client",

                    RequireConsent = true, // Not really needed if you're doing a custom deal, but nice to have.

                    // Impicit Grant
                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    // where to redirect to after login
                    RedirectUris = { "http://localhost:61802/signin-oidc" },

                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "http://localhost:61802/signout-callback-oidc" },
                    
                    // scopes that client has access to
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api_playground_1"
                    },

                    AllowOfflineAccess = true
                }
            };
        }

        internal static List<TestUser> GetTestUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "jason",
                    Password = "password",
                    Claims = new []
                    {
                        new Claim("name", "Jason"),
                        new Claim("website", "https://jasonkrause.net")
                    }
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "jeff",
                    Password = "password",
                    Claims = new []
                    {
                        new Claim("name", "Jeff"),
                        new Claim("website", "https://smartaccessit.com")
                    }
                }
            };
        }

        internal static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }
    }
}