using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using IdentityServer3.Core;
using IdentityServer3.Core.Services.InMemory;

namespace ComparerHostingService.Security
{
    public static class Users
    {
        public static List<InMemoryUser> Get()
        {
            var currentUser = new InMemoryUser
            {
                Username = "bob",
                Password = "secret",
                Subject = "1",
                Claims = new[]
                {
                    new Claim(Constants.ClaimTypes.GivenName, "Bob"),
                    new Claim(Constants.ClaimTypes.FamilyName, "Smith"),
                }
            };

            return new List<InMemoryUser>
            {
                currentUser
            };
        }
    }
}
