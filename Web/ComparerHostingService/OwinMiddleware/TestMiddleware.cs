using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace ComparerHostingService.OwinMiddleware
{
    using AppFunc = Func<IDictionary<string, object>, Task>;

    public class TestMiddleware
    {
        private AppFunc _next;

        public TestMiddleware(AppFunc next)
        {
            this._next = next;
        }

        public async Task Invoke(IDictionary<string, object> environment)
        {
            var context = new OwinContext(environment);

            context.Request.User = new GenericPrincipal(new GenericIdentity("NasC0"), new string[] { });

            Debug.Write("TestMiddleware: ");

            if (context.Authentication.User == null)
            {
                Debug.WriteLine("Anonymous");
            }
            else
            {
                Debug.WriteLine(context.Authentication.User.Identity.Name);
            }


            await this._next(environment);
        }
    }
}
