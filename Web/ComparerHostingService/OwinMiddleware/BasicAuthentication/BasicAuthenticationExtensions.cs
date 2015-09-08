using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Owin;

namespace ComparerHostingService.OwinMiddleware.BasicAuthentication
{
    public static class BasicAuthenticationExtensions
    {
        public static void UseBasicAuthentication(this IAppBuilder app, string realm,
            BasicAuthenticationMiddleware.CredentialValidationFunction validationFunction)
        {
            var options = new BasicAuthenticationOptions(realm, validationFunction);
            app.Use<BasicAuthenticationMiddleware>(options);
        }
    }
}
