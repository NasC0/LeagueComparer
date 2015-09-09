using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ComparerAuthenticationProofOfConcept.Infrastructure;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;

namespace ComparerAuthenticationProofOfConcept.OAuthServerProvider
{
    public class ApplicationOAuthServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            await Task.FromResult(context.Validated());
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var store = new MockUserStore();
            var user = await store.FindByEmailAsync(context.UserName);

            if (user == null || !store.PasswordIsValid(user, context.Password))
            {
                context.SetError("invalid_grant", "The username or password is incorrect");
                context.Rejected();
                return;
            }

            ClaimsIdentity identity = new ClaimsIdentity(context.Options.AuthenticationType);

            foreach (var userClaim in user.Claims)
            {
                identity.AddClaim(new Claim(userClaim.ClaimType, userClaim.ClaimValue));
            }

            context.Validated(identity);
        }
    }
}
