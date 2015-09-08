using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityServer3.Core.Services.InMemory;

namespace IdentityServerHost
{
    public static class Users
    {
        public static List<InMemoryUser> Get()
        {
            var firstUser = new InMemoryUser()
            {
                Username = "bob",
                Password = "secret",
                Subject = "1"
            };

            var secondUser = new InMemoryUser()
            {
                Username = "alice",
                Password = "secret",
                Subject = "2"
            };

            return new List<InMemoryUser>
            {
                firstUser,
                secondUser
            };
        }
    }
}
