using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ComparerAuthenticationProofOfConcept.Models;

namespace ComparerAuthenticationProofOfConcept.Infrastructure
{
    public class ApplicationDbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected async override void Seed(ApplicationDbContext context)
        {
            context.Companies.Add(new Company { Name = "Microsoft" });
            context.Companies.Add(new Company { Name = "Apple" });
            context.Companies.Add(new Company { Name = "Google" });
            context.SaveChanges();

            // Set up two initial users with different role claims:
            var john = new MockUser { Email = "john@example.com" };
            var jimi = new MockUser { Email = "jimi@Example.com" };

            john.Claims.Add(new MockUserClaims
            {
                ClaimType = ClaimTypes.Name,
                UserId = john.Id,
                ClaimValue = john.Email
            });
            john.Claims.Add(new MockUserClaims
            {
                ClaimType = ClaimTypes.Role,
                UserId = john.Id,
                ClaimValue = "Admin"
            });

            jimi.Claims.Add(new MockUserClaims
            {
                ClaimType = ClaimTypes.Name,
                UserId = jimi.Id,
                ClaimValue = jimi.Email
            });
            jimi.Claims.Add(new MockUserClaims
            {
                ClaimType = ClaimTypes.Role,
                UserId = john.Id,
                ClaimValue = "User"
            });

            var store = new MockUserStore();
            await store.AddUserAsync(john, "JohnsPassword");
            await store.AddUserAsync(jimi, "JimisPassword");
        }
    }
}
