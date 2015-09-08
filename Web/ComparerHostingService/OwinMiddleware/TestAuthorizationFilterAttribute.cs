using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace ComparerHostingService.OwinMiddleware
{
    public class TestAuthorizationFilterAttribute : AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            Debug.Write("AuthorizationFilter: ");

            if (actionContext.RequestContext.Principal == null)
            {
                Debug.WriteLine("Anonymous");
            }
            else
            {
                Debug.WriteLine(actionContext.RequestContext.Principal.Identity.Name);
            }

            return true;
        }
    }
}
