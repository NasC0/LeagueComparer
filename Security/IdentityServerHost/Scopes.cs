using System.Collections.Generic;
using IdentityServer3.Core.Models;

namespace IdentityServerHost
{
    public static class Scopes
    {
        public static ICollection<Scope> Get()
        {
            var leagueComparerScope = new Scope()
            {
                Name = "LeagueComparer"
            };

            return new List<Scope>
            {
                leagueComparerScope
            };
        }
    }
}
