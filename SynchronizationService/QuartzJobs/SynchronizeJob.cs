using System;
using System.Collections.Generic;
using ApiProcessing.Contracts;
using ApiProcessing.Enumerations;
using ApiProcessing.Queries;
using log4net;
using Logging;
using Quartz;

namespace SynchronizationService.QuartzJobs
{
    public class SynchronizeJob : IJob
    {
        public const string QueryBuilderArgumentName = "QueryBuilder";
        public const string QueryExecutorArgumentName = "QueryExecutor";
        public const string ProcessingStrategyFactoryArgumentName = "ProcessingStrategyFactory";

        private ILog logger;
        private IQueryBuilder queryBuilder;
        private IQueryExecutor queryExecutor;
        private IProcessingStrategyFactory processingStrategyFactory;

        public SynchronizeJob()
        {
            this.logger = SysLogger.GetLogger(typeof(SynchronizeJob));
        }

        public async void Execute(IJobExecutionContext context)
        {
            this.logger.Info("Starting database synchronization");
            this.InitializePrerequisites(context.JobDetail.JobDataMap);

            var allQueries = this.GetQueriesForGameObjects();

            foreach (var query in allQueries)
            {
                try
                {
                    var queryResponse = await this.queryExecutor.ExecuteQuery(query);
                    var currentQueryProcessingStrategy = this.processingStrategyFactory.GetProcessingStrategy(queryResponse.QueryObjectType);
                    await currentQueryProcessingStrategy.ProcessQueryResponse(queryResponse);
                }
                catch (Exception ex)
                {
                    this.logger.FatalFormat("Exception caught while trying to synchronize {0}s to the database: {1}", query.ObjectType.ToString(), ex);
                }
            }
        }

        private void InitializePrerequisites(JobDataMap dataMap)
        {
            try
            {
                this.logger.Info("Initializing prerequisites for database synchronization.");
                this.queryBuilder = (IQueryBuilder)dataMap[QueryBuilderArgumentName];
                this.queryExecutor = (IQueryExecutor)dataMap[QueryExecutorArgumentName];
                this.processingStrategyFactory = (IProcessingStrategyFactory)dataMap[ProcessingStrategyFactoryArgumentName];
            }
            catch (Exception ex)
            {
                this.logger.FatalFormat("Initializing prerequisites failed: {0}", ex);
                throw ex;
            }
        }

        private IEnumerable<IQuery> GetQueriesForGameObjects()
        {
            var allQueries = new List<IQuery>
            {
                this.queryBuilder.BuildQuery(ObjectType.Champion, this.GetChampionDefaultQueryParameter()),
                this.queryBuilder.BuildQuery(ObjectType.Item, this.GetItemDefaultQueryParameter()),
                this.queryBuilder.BuildQuery(ObjectType.Mastery, this.GetMasteryDefaultQueryParameter()),
                this.queryBuilder.BuildQuery(ObjectType.Rune, this.GetRuneDefaultQueryParameter())
            };

            return allQueries;
        }

        private ICollection<QueryParameter> GetChampionDefaultQueryParameter()
        {
            var queryParameters = new List<QueryParameter>()
            {
                new QueryParameter("champData", "all")
            };
            return queryParameters;
        }

        private ICollection<QueryParameter> GetItemDefaultQueryParameter()
        {
            var queryParameters = new List<QueryParameter>()
            {
                new QueryParameter("itemListData", "all")
            };
            return queryParameters;
        }

        private ICollection<QueryParameter> GetMasteryDefaultQueryParameter()
        {
            var queryParameters = new List<QueryParameter>()
            {
                new QueryParameter("masteryListData", "all")
            };
            return queryParameters;
        }

        private ICollection<QueryParameter> GetRuneDefaultQueryParameter()
        {
            var queryParameters = new List<QueryParameter>()
            {
                new QueryParameter("runeListData", "all")
            };
            return queryParameters;
        }
    }
}
