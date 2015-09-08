using System.Web.Http;
using System.Web.Http.Results;

namespace ComparerKatanaHosting
{
    public class GreetingController : ApiController
    {
        public IHttpActionResult Get()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            
            var greeting = new Greeting() {Text = "Teehee"};
            return Ok(greeting);
        }
    }
}
