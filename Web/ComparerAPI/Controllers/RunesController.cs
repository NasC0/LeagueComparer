using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using ComparerAPI.ViewModels;
using Data.Contracts;
using Models;

namespace ComparerAPI.Controllers
{
    public class RunesController : BaseController
    {
        private IRepository<Rune> _runesCollection; 

        public RunesController(IRepositoryFactory repoFactory)
            : base(repoFactory)
        {
            this._runesCollection = this._repoFactory.GetRepository<Rune>();
        }

        public async Task<IHttpActionResult> Get()
        {
            var runes = await this._runesCollection.All();
            var outputRunes = runes.AsQueryable()
                .Select(RuneOutputModel.FromModel);
            return Ok(outputRunes);
        }

        public async Task<IHttpActionResult> Get(int id)
        {
            var runes = await this._runesCollection.Find(r => r.RuneId == id);
            var outputRunes = runes.AsQueryable()
                .Select(RuneOutputModel.FromModel)
                .FirstOrDefault();
            return Ok(outputRunes);
        }
    }
}