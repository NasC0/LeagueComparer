using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Data.Contracts;
using Microsoft.AspNet.Identity;
using Models;

namespace ComparerAPI.Infrastructure
{
    public class MongoUserStore<TUser> : IUserLoginStore<TUser>,
        IUserClaimStore<TUser>, IUserRoleStore<TUser>,
        IUserPasswordStore<TUser>, IUserSecurityStampStore<TUser>,
        IUserStore<TUser>
        where TUser : User
    {
        private const string CollectionName = "AspNetUsers";

        private IRepository<User> _users;

        private bool _disposed;

        public MongoUserStore(IRepositoryFactory repoFactory)
        {
            _users = repoFactory.GetRepository<User>(CollectionName);
        }

        public async Task AddLoginAsync(TUser user, UserLoginInfo login)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            bool isLoginAdded = user.Logins.Any(u => u.LoginProvider == login.LoginProvider &&
                                                         u.ProviderKey == login.LoginProvider);

            if (!isLoginAdded)
            {
                user.Logins.Add(login);
            }
        }

        public async Task<TUser> FindAsync(UserLoginInfo login)
        {
            var users =
                 await _users.Find(
                    u => u.Logins.Any(l => l.LoginProvider == login.LoginProvider && l.ProviderKey == login.ProviderKey));

            TUser user = (TUser)users.FirstOrDefault();

            return user;
        }

        public async Task<IList<UserLoginInfo>> GetLoginsAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return user.Logins.ToList();
        }

        public async Task RemoveLoginAsync(TUser user, UserLoginInfo login)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.Logins.RemoveAll(l => l.LoginProvider == login.LoginProvider && l.ProviderKey == login.ProviderKey);
        }

        public async Task CreateAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            await _users.Add(user);
        }

        public async Task DeleteAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            await _users.Remove(user, u => u.UserName == user.UserName);
        }

        public async Task<TUser> FindByIdAsync(string userId)
        {
            ThrowIfDisposed();
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentNullException("userId");
            }

            var users = await this._users.Find(u => u.Id == userId);
            var user = (TUser)users.FirstOrDefault();
            return user;
        }

        public async Task<TUser> FindByNameAsync(string userName)
        {
            ThrowIfDisposed();
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentNullException("userName");
            }

            var users = await this._users.Find(u => u.UserName == userName);
            var user = (TUser)users.FirstOrDefault();
            return user;
        }

        public async Task UpdateAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            await this._users.Replace(user, u => u.UserName == user.UserName);
        }

        public void Dispose()
        {
            _users = null;
            _disposed = true;
        }

        public async Task AddClaimAsync(TUser user, Claim claim)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (!user.Claims.Any(x => x.ClaimType == claim.Type && x.ClaimValue == claim.Value))
            {
                user.Claims.Add(new IdentityUserClaim
                {
                    ClaimType = claim.Type,
                    ClaimValue = claim.Value
                });
            }
        }

        public async Task<IList<Claim>> GetClaimsAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            var result = user.Claims.Select(c => new Claim(c.ClaimType, c.ClaimValue)).ToList();
            return result;
        }

        public async Task RemoveClaimAsync(TUser user, Claim claim)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.Claims.RemoveAll(x => x.ClaimType == claim.Type && x.ClaimValue == claim.Value);
        }

        public async Task AddToRoleAsync(TUser user, string roleName)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            bool userHasRole = user.Roles.Contains(roleName, StringComparer.InvariantCultureIgnoreCase);
            if (!userHasRole)
            {
                user.Roles.Add(roleName);
            }
        }

        public async Task<IList<string>> GetRolesAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return user.Roles.ToList();
        }

        public async Task<bool> IsInRoleAsync(TUser user, string roleName)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            bool isUserInRole = user.Roles.Contains(roleName, StringComparer.InvariantCultureIgnoreCase);
            return isUserInRole;
        }

        public async Task RemoveFromRoleAsync(TUser user, string roleName)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.Roles.RemoveAll(r => String.Equals(r, roleName, StringComparison.InvariantCultureIgnoreCase));
        }

        public async Task<string> GetPasswordHashAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return user.PasswordHash;
        }

        public async Task<bool> HasPasswordAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            bool hasPassword = user.PasswordHash != null;
            return hasPassword;
        }

        public async Task SetPasswordHashAsync(TUser user, string passwordHash)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.PasswordHash = passwordHash;
        }

        public async Task<string> GetSecurityStampAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return user.SecurityStamp;
        }

        public async Task SetSecurityStampAsync(TUser user, string stamp)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.SecurityStamp = stamp;
        }

        private void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }
    }
}