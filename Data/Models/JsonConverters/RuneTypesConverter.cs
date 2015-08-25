using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Common.Enumerations;
using Newtonsoft.Json;

namespace Models.JsonConverters
{
    public class RuneTypesConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            bool isConvertable = typeof(RuneTypes).IsAssignableFrom(objectType);
            return isConvertable;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var currentValue = (string)reader.Value;
            var runeType = Enum.Parse(typeof(RuneTypes), currentValue);
            return runeType;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
