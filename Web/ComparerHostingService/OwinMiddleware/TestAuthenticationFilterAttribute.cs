using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace ComparerHostingService.OwinMiddleware
{
    public class TestAuthenticationFilterAttribute : Attribute, IAuthenticationFilter
    {
        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            Debug.Write("AuthenticationFilter: ");
            
            if (context.Principal == null)
            {
                Debug.WriteLine("Anonymous");
            }
            else
            {
                Debug.WriteLine(context.Principal.Identity.Name);
            }
            //throw new NotImplementedException();
        }

        public async Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            //throw new NotImplementedException();
        }

        public bool AllowMultiple
        {
            get { return false; }
        }
    }
}
