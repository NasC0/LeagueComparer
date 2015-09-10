using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Data.Contracts;

namespace ComparerAPI.Controllers
{
    public class BaseController : ApiController
    {
        protected IRepositoryFactory _repoFactory;

        public BaseController(IRepositoryFactory repoFactory)
        {
            this._repoFactory = repoFactory;
        }
    }
}