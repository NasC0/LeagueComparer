using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Common.Enumerations;
using Newtonsoft.Json;

namespace Models.JsonConverters
{
    public class ChampionResourceTypeConverter : JsonConverter
    {

        public override bool CanConvert(Type objectType)
        {
            bool isConvertable = typeof(ChampionResourceType).IsAssignableFrom(objectType);
            return isConvertable;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var currentValue = (string)reader.Value;
            var championResourceType = Enum.Parse(typeof(ChampionResourceType), currentValue);
            return championResourceType;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
