using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Models.JsonConverters
{
    public class GenericEnumConverter<T> : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            bool isConvertable = typeof(T).IsAssignableFrom(objectType);
            return isConvertable;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var currentValue = (string)reader.Value;
            var masteryType = Enum.Parse(typeof(T), currentValue);
            return masteryType;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
