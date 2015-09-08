using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Security.Infrastructure;

namespace ComparerHostingService.OwinMiddleware.BasicAuthentication
{
    public class BasicAuthenticationMiddleware : AuthenticationMiddleware<BasicAuthenticationOptions>
    {
        public delegate Task<IEnumerable<Claim>> CredentialValidationFunction(string username, string password);

        public BasicAuthenticationMiddleware(Microsoft.Owin.OwinMiddleware next, BasicAuthenticationOptions options)
            : base(next, options)
        {
        }

        protected override AuthenticationHandler<BasicAuthenticationOptions> CreateHandler()
        {
            return new BasicAuthenticationHandler(this.Options);
        }
    }
}
