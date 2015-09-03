using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ApiProcessing.Contracts;
using Quartz;
using Quartz.Impl;
using SynchronizationService.QuartzJobs;

namespace SynchronizationService
{
    public partial class SynchronizationService : ServiceBase
    {
        private IQueryExecutor queryExecutor;
        private IProcessingStrategyFactory strategyFactory;
        private IQueryBuilder queryBuilder;

        public SynchronizationService(IQueryExecutor queryExecutor, IProcessingStrategyFactory strategyFactory, IQueryBuilder queryBuilder)
        {
            this.InitializeComponent();

            this.queryExecutor = queryExecutor;
            this.strategyFactory = strategyFactory;
            this.queryBuilder = queryBuilder;
        }

        protected override void OnStart(string[] args)
        {
            System.Diagnostics.Debugger.Launch();

            var logPath = @"D:/teehee.txt";

            using (StreamWriter sw = new StreamWriter(logPath))
            {
                sw.WriteLine("teehee");
                sw.Close();
            }

            var synchronizationJob = JobBuilder.Create<SynchronizeJob>()
                                               .WithIdentity("SynchronizationJob")
                                               .Build();
            synchronizationJob.JobDataMap.Add(SynchronizeJob.QueryBuilderArgumentName, this.queryBuilder);
            synchronizationJob.JobDataMap.Add(SynchronizeJob.ProcessingStrategyFactoryArgumentName, this.strategyFactory);
            synchronizationJob.JobDataMap.Add(SynchronizeJob.QueryExecutorArgumentName, this.queryExecutor);

            var synchronizationTrigger = TriggerBuilder.Create()
                                                       .WithIdentity("SynchronizationTrigger")
                                                       .WithDailyTimeIntervalSchedule(
                                                            sch => sch.WithIntervalInMinutes(2)
                                                                      .OnEveryDay()
                                                                      .StartingDailyAt(new TimeOfDay(15, 51, 00))
                                                       )
                                                       .StartNow()
                                                       .Build();

            var schedulerFactory = new StdSchedulerFactory();
            var currentScheduler = schedulerFactory.GetScheduler();
            currentScheduler.Start();
            currentScheduler.ScheduleJob(synchronizationJob, synchronizationTrigger);
        }

        protected override void OnStop()
        {
            this.Dispose();
        }
    }
}
