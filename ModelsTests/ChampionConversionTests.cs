using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Models.Common;
using Models.Common.Champion;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ModelsTests
{
    [TestClass]
    public class ChampionConversionTests
    {
        [TestMethod]
        public void Converting_ValidJsonChampion_ToChampionObject()
        {
            var directory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            var championJsonObject = JObject.Parse(File.ReadAllText(@"C:\Projects\LeagueComparer\ModelsTests\resources\" + "FullChamp.json"));
            var championParsed = JsonConvert.DeserializeObject<Champion>(championJsonObject.ToString());
        }
    }
}
