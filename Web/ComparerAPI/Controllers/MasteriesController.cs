using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ComparerAPI.ViewModels;
using Data.Contracts;
using Models;

namespace ComparerAPI.Controllers
{
    [Authorize]
    public class MasteriesController : ApiController
    {
        private IRepository<Mastery> _masteriesCollection;

        public MasteriesController(IRepositoryFactory repoFactory)
        {
            this._masteriesCollection = repoFactory.GetRepository<Mastery>();
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            var allMasteries = await this._masteriesCollection.All();
            var outputMasteries = allMasteries.AsQueryable()
                .Select(MasteryOutputModel.FromModel);

            return Ok(outputMasteries);
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get(int id)
        {
            var allMasteries = await this._masteriesCollection.Find(m => m.MasteryId == id);
            var outputMasteries = allMasteries.AsQueryable()
                .Select(MasteryOutputModel.FromModel)
                .SingleOrDefault();

            return Ok(outputMasteries);
        }
    }
}
