using System;
using System.Collections.Generic;
using System.Linq;
using Models.JsonConverters;
using Newtonsoft.Json;

namespace Models.Common.Champion
{
    [JsonConverter(typeof(SpellEffectConverter))]
    public class SpellEffect
    {
        public string Key { get; set; }
        public IEnumerable<double> Value { get; set; }

        public override bool Equals(object obj)
        {
            var objAsSpellEffect = obj as SpellEffect;

            if (objAsSpellEffect == null)
            {
                return false;
            }

            bool areKeysEqual = this.Key == objAsSpellEffect.Key;
            bool areValuesEqual = this.Value.OrderBy(v => v).SequenceEqual(objAsSpellEffect.Value.OrderBy(v => v));

            if (areKeysEqual && areValuesEqual)
            {
                return true;
            }

            return false;
        }
    }
}
