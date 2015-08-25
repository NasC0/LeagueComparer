using System.Collections.Generic;
using ApiProcessing.Models.JsonConverters;
using Models.Common;
using Newtonsoft.Json;

namespace Models
{
    public class Rune : Entity
    {
        public IEnumerable<string> Tags { get; set; }
        [JsonProperty("id")]
        public int RuneId { get; set; }
        public string SanitizedDescription { get; set; }
        [JsonConverter(typeof(StatsJsonConverter))]
        public IEnumerable<Stat> Stats { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public Image Image { get; set; }
        [JsonProperty("rune")]
        public RuneType RuneType { get; set; }
    }
}
