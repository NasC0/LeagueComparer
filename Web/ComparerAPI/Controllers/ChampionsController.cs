using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Data.Contracts;
using Models;

namespace ComparerAPI.Controllers
{
    public class ChampionsController : ApiController
    {
        private IRepository<Champion> _championsCollection; 

        public ChampionsController(IRepositoryFactory repoFactory)
        {
            this._championsCollection = repoFactory.GetRepository<Champion>();
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            var allChamps = await this._championsCollection.All();
            return Ok(allChamps);
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get(int id)
        {
            var allChamps = await this._championsCollection.Find(c => c.ChampionId == id);
            return Ok(allChamps);
        }
    }
}
