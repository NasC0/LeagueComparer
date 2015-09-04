using System;
using System.ServiceProcess;
using ApiProcessing.Contracts;
using log4net;
using Logging;
using Quartz;
using Quartz.Impl;
using SynchronizationService.Properties;
using SynchronizationService.QuartzJobs;

namespace SynchronizationService
{
    public partial class SynchronizationService : ServiceBase
    {
        private ILog logger = SysLogger.GetLogger(typeof(SynchronizationService));
        private readonly IQueryBuilder queryBuilder;
        private readonly IQueryExecutor queryExecutor;
        private readonly IProcessingStrategyFactory strategyFactory;

        public SynchronizationService(IQueryExecutor queryExecutor, IProcessingStrategyFactory strategyFactory,
            IQueryBuilder queryBuilder)
        {
            InitializeComponent();

            this.queryExecutor = queryExecutor;
            this.strategyFactory = strategyFactory;
            this.queryBuilder = queryBuilder;
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                var synchronizationJob = JobBuilder.Create<SynchronizeJob>()
                    .WithIdentity("SynchronizationJob")
                    .Build();

                synchronizationJob.JobDataMap.Add(SynchronizeJob.QueryBuilderArgumentName, queryBuilder);
                synchronizationJob.JobDataMap.Add(SynchronizeJob.ProcessingStrategyFactoryArgumentName, strategyFactory);
                synchronizationJob.JobDataMap.Add(SynchronizeJob.QueryExecutorArgumentName, queryExecutor);

                var currentScheduleDateTime = Settings.Default.ScheduleTime;
                var scheduleTime = new TimeOfDay(currentScheduleDateTime.Hour, currentScheduleDateTime.Minute, currentScheduleDateTime.Second);

                var synchronizationTrigger = TriggerBuilder.Create()
                                                        .WithIdentity("SynchronizationTrigger")
                                                        .WithDailyTimeIntervalSchedule(
                                                            sch => sch.WithIntervalInHours(Settings.Default.SynchronizationIntervalInHours)
                                                                      .OnEveryDay()
                                                                      .StartingDailyAt(scheduleTime)
                                                                      .InTimeZone(TimeZoneInfo.Local)
                                                        )
                                                        .StartNow()
                                                        .Build();

                var schedulerFactory = new StdSchedulerFactory();
                var currentScheduler = schedulerFactory.GetScheduler();
                currentScheduler.Start();
                var fireTime = currentScheduler.ScheduleJob(synchronizationJob, synchronizationTrigger);

                this.logger.InfoFormat("API processing job scheduled successfully. Job will fire at {0} in local time and {1} in UTC time",
                    fireTime.ToLocalTime(), fireTime);
            }
            catch (Exception ex)
            {
                this.logger.FatalFormat("Exception fired while setting up API processing job schedule: {0}", ex);
                throw ex;
            }

            base.OnStart(args);
        }

        protected override void OnStop()
        {
            Dispose();
            base.OnStop();
        }
    }
}