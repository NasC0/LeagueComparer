using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiProcessing.Contracts;
using Data.Contracts;
using Models;

namespace ApiProcessing.Processing
{
    public class ChampionProcessingStrategy : BaseProcessingStrategy<Champion>, IGameObjectProcessingStrategy
    {
        public ChampionProcessingStrategy(IRepository<Champion> championRepo)
            : base(championRepo, typeof(ChampionProcessingStrategy))
        {
        }

        public Task ProcessQueryResponse(IQueryResponse response)
        {
            throw new NotImplementedException();
        }
    }
}
