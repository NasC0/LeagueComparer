using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiProcessing.Contracts;
using ApiProcessing.Enumerations;
using ApiProcessing.Queries;
using log4net;
using Logging;

namespace SynchronizationService.Infrastructure
{
    public static class SynchronizationExecutor
    {
        private const string ExceptionEmailSubject = "LeagueComparer Database Synchronization Exception Caught";

        private static ILog logger = SysLogger.GetLogger(typeof (SynchronizationExecutor));

        public async static Task ExecuteSynchronization(IQueryExecutor queryExecutor, IQueryBuilder queryBuilder,
            IProcessingStrategyFactory processingStrategyFactory)
        {
            var allQueries = GetQueriesForGameObjects(queryBuilder);

            foreach (var query in allQueries)
            {
                try
                {
                    var queryResponse = await queryExecutor.ExecuteQuery(query);
                    var currentQueryProcessingStrategy = processingStrategyFactory.GetProcessingStrategy(queryResponse.QueryObjectType);
                    await currentQueryProcessingStrategy.ProcessQueryResponse(queryResponse);
                }
                catch (Exception ex)
                {
                    string fatalFormat = string.Format("Exception caught while trying to synchronize {0}s to the database: {1}",
                            query.ObjectType, ex);
                    logger.FatalFormat(fatalFormat);
                    EmailNotifier.SendExceptionEmail(ExceptionEmailSubject, fatalFormat);
                }
            }
        }

        private static IEnumerable<IQuery> GetQueriesForGameObjects(IQueryBuilder queryBuilder)
        {
            var allQueries = new List<IQuery>
            {
                queryBuilder.BuildQuery(ObjectType.Champion, GetChampionDefaultQueryParameter()),
                queryBuilder.BuildQuery(ObjectType.Item, GetItemDefaultQueryParameter()),
                queryBuilder.BuildQuery(ObjectType.Mastery, GetMasteryDefaultQueryParameter()),
                queryBuilder.BuildQuery(ObjectType.Rune, GetRuneDefaultQueryParameter())
            };

            return allQueries;
        }

        private static QueryParameter GetChampionDefaultQueryParameter()
        {
            var queryParameter = new QueryParameter("champData", "all");
            return queryParameter;
        }

        private static QueryParameter GetItemDefaultQueryParameter()
        {
            var queryParameter = new QueryParameter("itemListData", "all");
            return queryParameter;
        }

        private static QueryParameter GetMasteryDefaultQueryParameter()
        {
            var queryParameter = new QueryParameter("masteryListData", "all");
            return queryParameter;
        }

        private static QueryParameter GetRuneDefaultQueryParameter()
        {
            var queryParameter = new QueryParameter("runeListData", "all");
            return queryParameter;
        }
    }
}
