using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Configuration
{
    public static class ConfigurationManager
    {
        public static Config GetCurrentConfiguration(string fileLocation)
        {
            var apiDataDict = JsonConvert.DeserializeObject<IDictionary<string, string>>(File.ReadAllText(fileLocation));
            var configResult = new Config(apiDataDict);
            return configResult;
        }
    }
}
