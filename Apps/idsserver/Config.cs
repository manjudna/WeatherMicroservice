using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Duende.IdentityServer;
using Duende.IdentityServer.Models;


namespace idsserver
{
    public static class Config
    {

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource>
            {
                new ApiResource("WeatherApi"){ Scopes = { "WeatherApi.read" } }
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("WeatherApi.read", "Read access to Api")
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                // machine to machine client
                new Client
                {
                    ClientId = "m2m.client",
                    ClientSecrets = { new Secret("SuperSecretPassword".Sha256()) },

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    // scopes that client has access to
                    AllowedScopes = { "WeatherApi.read" }
                },
                new Client
                    {
                        ClientId = "interactive.public",

                        AllowedGrantTypes = GrantTypes.Code,
                        // this client is SPA, and therefore doesn't need client secret
                        RequireClientSecret = false,

                        RedirectUris = { "http://localhost:4200" },
                        PostLogoutRedirectUris = { "http://localhost:4200" },
                        AllowOfflineAccess = true,

                        AllowedScopes = { "openid", "profile", "weatherapi.read" }
                    },
                // interactive ASP.NET Core MVC client
                new Client
                {
                    ClientId = "mvc",
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Code,
                    
                    // where to redirect to after login
                    RedirectUris = { "https://localhost:5006/signin-oidc" },

                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "https://localhost:5006/signout-callback-oidc" },

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "WeatherApi.read"
                    }
                    ,
                    AllowOfflineAccess = true
                }
            };

    }
        
}
