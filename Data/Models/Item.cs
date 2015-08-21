using System.Collections.Generic;
using ApiProcessing.Models.JsonConverters;
using Models.Common;
using Newtonsoft.Json;

namespace Models
{
    public class Item : Entity
    {
        public IEnumerable<string> Tags { get; set; }

        [JsonProperty("id")]
        public int ItemId { get; set; }
        public string SanitizedDescription { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public Image Image { get; set; }
        public Gold Gold { get; set; }
        public IEnumerable<int> From { get; set; }
        public IEnumerable<int> Into { get; set; }
        [JsonConverter(typeof(StatsJsonConverter))]
        public IEnumerable<Stat> Stats { get; set; }
        public string Group { get; set; }
        public int Depth { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
