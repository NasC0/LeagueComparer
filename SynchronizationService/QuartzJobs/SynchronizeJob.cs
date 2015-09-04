using System;
using System.Collections.Generic;
using ApiProcessing.Contracts;
using ApiProcessing.Enumerations;
using ApiProcessing.Queries;
using log4net;
using Logging;
using Quartz;
using SynchronizationService.Infrastructure;

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
            await SynchronizationExecutor.ExecuteSynchronization(this.queryExecutor, this.queryBuilder,
                    this.processingStrategyFactory);
        }

        private void InitializePrerequisites(JobDataMap dataMap)
        {
            try
            {
                this.queryBuilder = (IQueryBuilder)dataMap[QueryBuilderArgumentName];
                this.queryExecutor = (IQueryExecutor)dataMap[QueryExecutorArgumentName];
                this.processingStrategyFactory = (IProcessingStrategyFactory)dataMap[ProcessingStrategyFactoryArgumentName];
                this.logger.Info("Initialized prerequisites for database synchronization.");
            }
            catch (Exception ex)
            {
                this.logger.FatalFormat("Initializing prerequisites failed: {0}", ex);
                throw ex;
            }
        }
    }
}
