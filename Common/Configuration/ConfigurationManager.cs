using System.Collections.Generic;
using System.IO;
using Logging;
using log4net;
using Newtonsoft.Json;
using System;

namespace Configuration
{
    public static class ConfigurationManager
    {
        private static ILog logger = SysLogger.GetLogger(typeof(ConfigurationManager));

        public static Config GetCurrentConfiguration(string fileLocation)
        {
            try
            {
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileLocation);

                string jsonConfig;
                using (var sr = new StreamReader(filePath))
                {
                    jsonConfig = sr.ReadToEnd();
                }

                var apiDataDict = JsonConvert.DeserializeObject<IDictionary<string, string>>(jsonConfig);
                var configResult = new Config(apiDataDict);

                logger.Info("Configuration built successfully");
                return configResult;
            }
            catch(IOException ex)
            {
                logger.FatalFormat("Fatal exception met while looking for configuration file: {0}", ex);
                throw ex;
            }
            catch(Exception ex)
            {
                logger.FatalFormat("Fatal exception met while building configuration: {0}", ex);
                throw ex;
            }
        }
    }
}