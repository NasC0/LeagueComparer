using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ComparerAuthenticationProofOfConcept.Infrastructure;
using Data.Contracts;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Models;
using Ninject;
using Microsoft.AspNet.Identity.Owin;

namespace ComparerAuthenticationProofOfConcept.OAuthServerProvider
{
    public class ApplicationOAuthServerProvider : OAuthAuthorizationServerProvider
    {
        private IKernel _diKernel;

        public ApplicationOAuthServerProvider(IKernel kernel)
        {
            this._diKernel = kernel;
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            await Task.FromResult(context.Validated());
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();
            var user = await userManager.FindAsync(context.UserName, context.Password);

            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect");
                context.Rejected();
                return;
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            foreach (var userClaim in user.Claims)
            {
                identity.AddClaim(new Claim(userClaim.ClaimType, userClaim.ClaimValue));
            }

            context.Validated(identity);
        }
    }
}
