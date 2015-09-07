using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Config;

namespace Logging
{
    public static class SysLogger
    {
        private static readonly string configFilePath = "../../config";
        private static readonly string loggerConfigFileName = "log4net.config";

        public static ILog GetLogger(Type classType)
        {
            InitializeConfiguration();
            var logger = LogManager.GetLogger(classType);
            return logger;
        }

        public static ILog GetLogger(string loggerName)
        {
            InitializeConfiguration();
            var logger = LogManager.GetLogger(loggerName);
            return logger;
        }

        private static void InitializeConfiguration()
        {
            if (!LogManager.GetRepository().Configured)
            {
                string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, configFilePath + "/" + loggerConfigFileName);
                var loggerConfigFile = new FileInfo(filePath);
                XmlConfigurator.ConfigureAndWatch(loggerConfigFile);
            }
        }
    }
}
