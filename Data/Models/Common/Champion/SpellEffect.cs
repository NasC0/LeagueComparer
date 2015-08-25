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
    }
}
