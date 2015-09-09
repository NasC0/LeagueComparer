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
    public interface IDbContext
    {
        IDbSet<Company> Companies { get; set; }
        IDbSet<MockUser> Users { get; set; }
        IDbSet<MockUserClaims> Claims { get; set; }

        int SaveChanges();
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
