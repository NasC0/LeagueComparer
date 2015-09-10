using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Common.Champion;
using Newtonsoft.Json;

namespace Models.JsonConverters
{
    public class SpellEffectWriter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var valueAsList = value as IEnumerable<SpellEffect>;
            var valueAsSingle = value as SpellEffect;
        }
    }
}
