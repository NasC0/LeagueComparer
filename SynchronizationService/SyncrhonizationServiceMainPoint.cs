using System;
using System.Diagnostics;
using System.Reflection;
using System.ServiceProcess;
using ApiProcessing.Contracts;
using Ninject;

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
            Debugger.Launch();
            if (!Environment.UserInteractive)
            {
                kernel = new StandardKernel();
                kernel.Load(Assembly.GetExecutingAssembly());

                var strategyFactory = kernel.Get<IProcessingStrategyFactory>();
                var queryBuilder = kernel.Get<IQueryBuilder>();
                var queryExecutor = kernel.Get<IQueryExecutor>();

                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[] 
                { 
                    new SynchronizationService(queryExecutor, strategyFactory, queryBuilder) 
                };
                ServiceBase.Run(ServicesToRun);
            }
            else
            {
                Console.WriteLine("TEEHEE");
            }
        }
    }
}
