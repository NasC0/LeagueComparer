using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Common.Champion;
using Newtonsoft.Json;

namespace Models.JsonConverters
{
    public class SpellEffectConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var effectNameCounter = 0;
            List<SpellEffect> spellEffects = new List<SpellEffect>();

            if (reader.Value == null)
            {
                spellEffects.Add(new SpellEffect()
                    {
                        Key = "e" + effectNameCounter,
                        Value = null
                    });
            }

            return spellEffects;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
