using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComparerAuthenticationProofOfConcept.Models;

namespace ComparerAuthenticationProofOfConcept.Infrastructure
{
    public class MockUserStore
    {
        private IDbContext _dbContext;

        public MockUserStore()
            : this(new ApplicationDbContext())
        {
        }

        public MockUserStore(IDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task AddUserAsync(MockUser user, string password)
        {
            var userExsits = await UserExists(user);
            if (userExsits)
            {
                throw new Exception(
                    "A user with that Email address already exists");
            }
            var hasher = new MockPasswordHasher();
            user.PasswordHash = hasher.CreateHash(password).ToString();
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
        }


        public async Task<MockUser> FindByEmailAsync(string email)
        {
            var currentUser = await this._dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            return currentUser;
        }


        public async Task<MockUser> FindByIdAsync(string userId)
        {
            return await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Id == userId);
        }


        public async Task<bool> UserExists(MockUser user)
        {
            return await _dbContext.Users
                .AnyAsync(u => u.Id == user.Id || u.Email == user.Email);
        }


        public async Task AddClaimAsync(string UserId, MockUserClaims claim)
        {
            var user = await FindByIdAsync(UserId);
            if (user == null)
            {
                throw new Exception("User does not exist");
            }
            user.Claims.Add(claim);
            await _dbContext.SaveChangesAsync();
        }


        public bool PasswordIsValid(MockUser user, string password)
        {
            var hasher = new MockPasswordHasher();
            var hash = hasher.CreateHash(password);
            return hash.Equals(user.PasswordHash);
        }
    }
}
