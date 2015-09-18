using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Models
{
    public class User : IUser<String>
    {
        public User(string username)
            : this()
        {
            this.UserName = username;
        }

        public User()
        {
            this.Roles = new List<string>();
            this.Claims = new List<IdentityUserClaim>();
            this.Logins = new List<UserLoginInfo>();
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public virtual string Id { get; set; }

        public virtual string UserName { get; set; }

        public virtual string Email { get; set; }

        public virtual bool EmailConfirmed { get; set; }

        public virtual string PasswordHash { get; set; }

        public virtual string SecurityStamp { get; set; }

        public virtual List<string> Roles { get; set; }

        public virtual List<IdentityUserClaim> Claims { get; set; }

        public virtual List<UserLoginInfo> Logins { get; set; }

        public virtual bool HasLocalAccount { get; set; }
        
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class IdentityUserClaim
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public virtual string Id { get; set; }

        public virtual string UserId { get; set; }

        public virtual string ClaimType { get; set; }

        public virtual string ClaimValue { get; set; }
    }
}
