using System.Linq;
using System.Security.Claims;
using Data.Contracts;
using Models;

namespace ComparerAuthenticationProofOfConcept.Infrastructure
{
    public static class SeedMongoDatabase
    {
        public async static void Seed(IRepositoryFactory repoFactory)
        {
            var users = repoFactory.GetRepository<User>("AspNetUsers");

            var allUsers = await users.All();
            if (!allUsers.Any())
            {
                var john = new User("john@example.com");
                var userStore = new MongoUserStore<User>(repoFactory);
                var userManager = new ApplicationUserManager(userStore);
                var jimi = new User("jimi@example.com");

                var johnResult = await userManager.CreateAsync(john, "JohnsPassword");
                var jimiResult = await userManager.CreateAsync(jimi, "JimisPassword");

                await userManager.AddClaimAsync(john.Id, new Claim(ClaimTypes.Name, "john@example.com"));
                await userManager.AddClaimAsync(john.Id, new Claim(ClaimTypes.Role, "Admin"));

                await userManager.AddClaimAsync(jimi.Id, new Claim(ClaimTypes.Name, "jimi@example.com"));
                await userManager.AddClaimAsync(jimi.Id, new Claim(ClaimTypes.Role, "User"));
            }
        }
    }
}
