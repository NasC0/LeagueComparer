using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using ApiProcessing.Contracts;
using ApiProcessing.Queries;
using Configuration;
using Helpers;
using Ninject;

namespace SynchronizationService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            System.Diagnostics.Debugger.Launch();
            IKernel kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());

            var masterConfiguration = ConfigurationManager.GetCurrentConfiguration(Properties.Settings.Default.MasterConfigLocation);

            var strategyFactory = kernel.Get<IProcessingStrategyFactory>();
            var queryBuilder = new QueryBuilder(Properties.Settings.Default.ApiKey, ApiUrlBuilder.BuildApiStaticDataUrl(Regions.euw, masterConfiguration));
            var queryExecutor = kernel.Get<IQueryExecutor>();


            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new SynchronizationService(queryExecutor, strategyFactory, queryBuilder) 
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
