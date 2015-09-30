using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using ComparerAPI.ViewModels;
using Data.Contracts;
using Microsoft.Ajax.Utilities;
using Models;

namespace ComparerAPI.Controllers
{
    [Authorize]
    [EnableCors("*", "*", "*", "*")]
    public class ItemsController : ApiController
    {
        private IRepository<Item> _items; 

        public ItemsController(IRepositoryFactory repoFactory)
        {
            this._items = repoFactory.GetRepository<Item>();
        }

        public async Task<IHttpActionResult> Get()
        {
            try
            {
                var allItems = await this._items.All();
                var outputItems = allItems.AsQueryable()
                    .Select(ItemOutputModel.FromModel);

                return Ok(outputItems);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                var allItems = await this._items.Find(i => i.ItemId == id);
                var outputItem = allItems.AsQueryable()
                    .Select(ItemOutputModel.FromModel)
                    .FirstOrDefault();

                return Ok(outputItem);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
