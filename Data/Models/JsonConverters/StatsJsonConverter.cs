using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Models;
using Models.Common;
using MongoDB.Bson;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ApiProcessing.Models.JsonConverters
{
    public class StatsJsonConverter : JsonConverter
    {
        private IStatConverter statConverter;

        public StatsJsonConverter()
            : this(new StatConverter())
        {
        }

        public StatsJsonConverter(IStatConverter statConverter)
        {
            this.statConverter = statConverter;
        }

        public override bool CanConvert(Type objectType)
        {
            bool isConvertible = typeof (Item) == objectType;
            return isConvertible;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var statsJsonObject = JObject.Load(reader);
            var statsJsonCollection = new List<Tuple<string, double>>();

            foreach (var stat in statsJsonObject)
            {
                double statValue = stat.Value.Value<double>();

                statsJsonCollection.Add(new Tuple<string, double>(stat.Key, statValue));
            }

            return this.statConverter.ConvertStats(statsJsonCollection);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var valueConverted = (IEnumerable<Stat>) value;
            JToken writeObject;
            if (valueConverted.Any())
            {
                writeObject = JToken.FromObject(valueConverted);
            }
            else
            {
                writeObject = JToken.FromObject(new List<Stat>());
            }

            writeObject.WriteTo(writer);
        }
    }
}
