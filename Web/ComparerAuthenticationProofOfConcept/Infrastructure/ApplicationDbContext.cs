using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ComparerAuthenticationProofOfConcept.Models;

namespace ComparerAuthenticationProofOfConcept.Infrastructure
{
    public class ApplicationDbContext : DbContext, IDbContext
    {
        public ApplicationDbContext()
            : base("ComparerProofOfConcept")
        {
        }

        static ApplicationDbContext()
        {
            Database.SetInitializer(new ApplicationDbInitializer());
        }

        public IDbSet<Company> Companies { get; set; }


        public IDbSet<MockUser> Users { get; set; }

        public IDbSet<MockUserClaims> Claims { get; set; }
    }
}
