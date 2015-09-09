using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComparerAuthenticationProofOfConcept.Models
{
    public class MockUser
    {
        public MockUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Claims = new List<MockUserClaims>();
        }

        [Key]
        public string Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public virtual ICollection<MockUserClaims> Claims { get; set; }
    }

    public class MockUserClaims
    {
        public MockUserClaims()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }

        public string UserId { get; set; }

        public string ClaimType { get; set; }

        public string ClaimValue { get; set; }
    }

    public class MockPasswordHasher
    {
        public string CreateHash(string password)
        {
            var chars = password.ToArray();
            var hash = chars.Reverse().ToArray();
            return new string(hash);
        }
    }
}
