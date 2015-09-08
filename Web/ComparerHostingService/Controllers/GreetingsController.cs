using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using ComparerHostingService.OwinMiddleware;

namespace ComparerHostingService.Controllers
{
    public class GreetingsController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get()
        {
            Debug.Write("GreetingsController: ");

            if (User == null)
            {
                Debug.WriteLine("Anonymous");
            }
            else
            {
                Debug.WriteLine(User.Identity.Name);
            }

            return Ok(User);
        }
    }
}
