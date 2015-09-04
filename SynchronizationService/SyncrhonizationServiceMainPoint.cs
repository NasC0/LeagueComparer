using System;
using System.Diagnostics;
using System.Reflection;
using System.ServiceProcess;
using ApiProcessing.Contracts;
using Ninject;
using SynchronizationService.Infrastructure;

namespace SynchronizationService
{
    static class SyncrhonizationServiceMainPoint
    {
        private static IKernel kernel;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());

            var strategyFactory = kernel.Get<IProcessingStrategyFactory>();
            var queryBuilder = kernel.Get<IQueryBuilder>();
            var queryExecutor = kernel.Get<IQueryExecutor>();

            if (!Environment.UserInteractive)
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[] 
                { 
                    new SynchronizationService(queryExecutor, strategyFactory, queryBuilder) 
                };
                ServiceBase.Run(ServicesToRun);
            }
            else
            {
                SynchronizationExecutor.ExecuteSynchronization(queryExecutor, queryBuilder, strategyFactory).Wait();
            }
        }
    }
}
