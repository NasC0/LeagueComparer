using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityServer3.Core.Models;

namespace IdentityServerHost
{
    public static class Clients
    {
        public static List<Client> Get()
        {
            var currentClient = new Client
            {
                ClientName = "Silicon-only Client",
                ClientId = "silicon",
                Enabled = true,
                AccessTokenType = AccessTokenType.Reference,

                Flow = Flows.ClientCredentials,

                ClientSecrets = new List<Secret>
                    {
                        new Secret("F621F470-9731-4A25-80EF-67A6F7C5F4B8".Sha256())
                    },

                AllowedScopes = new List<string>
                    {
                        "LeagueComparer"
                    }
            };

            var secondClient = new Client
            {
                ClientName = "Silicon on behalf of Carbon Client",
                ClientId = "carbon",
                Enabled = true,
                AccessTokenType = AccessTokenType.Reference,

                Flow = Flows.ResourceOwner,

                ClientSecrets = new List<Secret>
                {
                    new Secret("21B5F798-BE55-42BC-8AA8-0025B903DC3B".Sha256())
                },

                AllowedScopes = new List<string>
                {
                    "LeagueComparer"
                }
            };

            return new List<Client>
            {
                currentClient,
                secondClient
            };
        }
    }
}
