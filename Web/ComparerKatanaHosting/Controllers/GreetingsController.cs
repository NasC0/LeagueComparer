using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace ComparerKatanaHosting.Controllers
{
    public class GreetingsController : ApiController
    {
        public Greeting Get()
        {
            return new Greeting { Text = "TEEHEE!" };
        }
    }
}
