using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityServer3.Core.Models;

namespace ComparerHostingService.Security
{
    public static class Clients
    {
        public static IEnumerable<Client> Get()
        {
            var currentClient = new Client
            {
                Enabled = true,
                ClientName = "LeagueComparer",
                ClientId = "comparer",
                Flow = Flows.Implicit,

                RedirectUris = new List<string>
                {
                    "https://localhost:44319"
                },

                AllowAccessToAllScopes = true
            };

            return new List<Client>()
            {
                currentClient
            };
        }
    }
}
