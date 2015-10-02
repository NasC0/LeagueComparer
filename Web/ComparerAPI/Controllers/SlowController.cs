using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace ComparerAPI.Controllers
{
    [Authorize]
    public class SlowController : ApiController
    {
        public async Task<IHttpActionResult> Get()
        {
            Thread.Sleep(2000);
            return Ok();
        }
    }
}
