using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Infrastructure;

namespace ComparerHostingService.OwinMiddleware.BasicAuthentication
{
    public class BasicAuthenticationHandler : AuthenticationHandler<BasicAuthenticationOptions>
    {
        private string _challenge;

        public BasicAuthenticationHandler(BasicAuthenticationOptions options)
        {
            this._challenge = "Basic realm=" + options.Realm;
        }

        protected async override Task<AuthenticationTicket> AuthenticateCoreAsync()
        {
            var authzValue = Request.Headers.Get("Authorization");
            if (string.IsNullOrEmpty(authzValue) || !authzValue.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            var token = authzValue.Substring("Basic ".Length).Trim();
            var claims = await TryGetPrincipalFromBasicCredentials(token, this.Options.CredentialValidationFunction);

            if (claims == null)
            {
                return null;
            }
            else
            {
                var id = new ClaimsIdentity(claims, this.Options.AuthenticationType);
                return new AuthenticationTicket(id, new AuthenticationProperties());
            }
        }

        protected override Task ApplyResponseChallengeAsync()
        {
            if (Response.StatusCode == 401)
            {
                var challenge = Helper.LookupChallenge(this.Options.AuthenticationType, this.Options.AuthenticationMode);
                if (challenge != null)
                {
                    Response.Headers.AppendValues("WWW-Authenticate", this._challenge);
                }
            }

            return Task.FromResult<object>(null);
        }

        private async Task<IEnumerable<Claim>> TryGetPrincipalFromBasicCredentials(string credentials,
            BasicAuthenticationMiddleware.CredentialValidationFunction credentialValidationFunction)
        {
            string pair;
            try
            {
                pair = Encoding.UTF8.GetString(Convert.FromBase64String(credentials));
            }
            catch (FormatException)
            {
                return null;
            }
            catch (ArgumentException)
            {
                return null;
            }

            var ix = pair.IndexOf(':');
            if (ix == -1)
            {
                return null;
            }

            var username = pair.Substring(0, ix);
            var password = pair.Substring(ix + 1);

            return await credentialValidationFunction(username, password);
        }
    }
}
