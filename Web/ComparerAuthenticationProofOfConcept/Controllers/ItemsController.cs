using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using ComparerAuthenticationProofOfConcept.Infrastructure;
using Data.Contracts;
using Models;

namespace ComparerAuthenticationProofOfConcept.Controllers
{
    public class ItemsController : ApiController
    {
        public IRepositoryFactory factory;

        public ItemsController(IRepositoryFactory factory)
        {
            this.factory = factory;
            var items = factory.GetRepository<Mastery>();
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetItems()
        {
            return Ok("teehee");
        }
    }
}
