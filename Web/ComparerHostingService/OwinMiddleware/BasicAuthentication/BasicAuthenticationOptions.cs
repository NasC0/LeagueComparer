using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Security;

namespace ComparerHostingService.OwinMiddleware.BasicAuthentication
{
    public class BasicAuthenticationOptions : AuthenticationOptions
    {
        public BasicAuthenticationOptions(string realm, BasicAuthenticationMiddleware.CredentialValidationFunction credentialValidationFunction) 
            : base("Basic")
        {
            this.Realm = realm;
            this.CredentialValidationFunction = credentialValidationFunction;
            this.AuthenticationMode = AuthenticationMode.Active;
        }

        public string Realm { get; set; }
        public BasicAuthenticationMiddleware.CredentialValidationFunction CredentialValidationFunction { get; set; }
    }
}
